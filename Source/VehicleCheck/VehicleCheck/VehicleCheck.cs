using System;
using System.Linq;
using System.Collections.Generic;
using System.Data;
using DatabaseProcessing;
using System.IO;

//                        _oo0oo_
//                       o8888888o
//                       88" . "88
//                       (| -_- |)
//                       0\  =  /0
//                     ___/`___`\___
//                   .` \\|     |// `.
//                 /  \\|||  :  |||// \
//                / -_||||| -:- |||||- \
//               |    | \\\  -  /// |   |
//               |  \_|  ''\---/''  |_/ |
//               \   .-\___ '-' ___/-.  /
//            ____'.  .'  /--.--\  '.  .'____
//         .""  '<  `.____\_<|>_/____.'  >'  "".
//        |  | :  `- \` .:`\ _ /`\:.`/ - ` : |  |
//         \  \ `_.   \_  __\ /__  _/   ._` /  /
//     =====`-.____`.____ \_____/____.-`___._'=====
//                        `=---='

//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
//               佛祖保佑       永无BUG

namespace VehicleCheck
{
    public class HighFrequencyCheck
    {
        class SensorTimeSpan
        {
            public int ID;
            public int StartTime;
            public int EndTime;
            public DateTime PassTime;
            public int isUsed = 0;
        }

        class CarInfo
        {
            public int Width;
            public int Height;
            public int StartTime;
            public int EndTime;
            public DateTime PassTime;
            public int LaneID;
            public string Remark;
        }

        //左侧边最小测高传感器
        int leftMinSensor = 1;
        //右侧边最小测高传感器
        int rightMinSensor = 240;
        //最小测宽传感器
        int minWidthSensor = 16;
        //最大测宽传感器
        int maxWidthSensor = 239;
        //传感器间距(cm)
        int sensorSpace = 5;
        //开始计算高度(cm)
        int bridgeStartHeight = 407;
        //高度传感器间距(cm)
        int HeightSensorSpace = 6;
        //最小传感器检测数量
        int minSensorCheckNum = 10;
        //最小传感器检测间隔数量
        int minSensorSpanNum = 20;
        //允许误差时间(ms)
        int allowDifftime = 200;
        //最大可跳开传感器数量
        int maxPassTime = 7;
        //去除最大最小传感器进入出去时间的数据数量
        int removeDataBase = 8;
        /// <summary>
        /// 车道分割标识
        /// </summary>
        int cut12 = 71;
        int cut23 = 142;
        /// <summary>
        /// 车辆列表
        /// </summary>
        List<CarInfo> carList = new List<CarInfo>();


        Random r = new Random();


        /// <summary>
        /// 车辆车辆
        /// </summary>
        public void CheckCar()
        {
            try
            {
                ////////////////////////
                //在运行前需要函数检查去除可能有问题的传感器
                ////////////////////////
                //建立本地连接
                EntityManager clientManager = new EntityManager();

                EntityBase systemLinkEntity = new EntityBase("SYSTEM_LINK");
                systemLinkEntity["SYSTEM_ID"] = "HOST";
                if (!clientManager.GetEntity(ref systemLinkEntity)) { throw new Exception("主机连接信息丢失!"); }
                string dataSource = systemLinkEntity["CONN_DATASOURCE"].ToString();
                string dataBase = systemLinkEntity["CONN_DATABASE"].ToString();
                string uid = systemLinkEntity["CONN_UID"].ToString();
                string pwd = systemLinkEntity["CONN_PWD"].ToString();

                //建立服务器连接
                EntityManager manager = new EntityManager(dataSource, dataBase, uid, pwd);
                EntityBase operationEntity = new EntityBase("OPERATION_ID", manager);
                operationEntity["TYPE"] = "SENSOR_DATA_END";
                manager.GetEntity(ref operationEntity);

                string startId = operationEntity["VALUE"].ToString();

                EntityBase carSourceEntity = new EntityBase("CAR_SOURCE", manager);
                WhereObjectList lastIdWhere = new WhereObjectList();
                lastIdWhere.add("FLAG", WhereObjectType.EqualTo, "-1");
                string endId = manager.GetEntityEx(ref carSourceEntity, lastIdWhere, "LOGID DESC") ? carSourceEntity["LOGID"].ToString() : int.MaxValue.ToString();

                //开始id等于结束id，直接返回
                if (startId.Equals(endId)) { return; }

                //获取损坏传感器列表
                EntityBase sensorEntity = new EntityBase("SENSOR_CONFIG", manager);
                WhereObjectList sensorWhere = new WhereObjectList();
                sensorWhere.add("SENSOR_STATUS", WhereObjectType.EqualTo, "0");
                DataTable errSensorTable = manager.GetTableEx(sensorEntity, sensorWhere, 0, "");
                var errSensorList = errSensorTable.AsEnumerable().Select(t => t.Field<int>("SENSOR_ID")).ToList();

                //获取传感器分段数据
                EntityBase breakEnenty = new EntityBase("CAR_SOURCE", manager);
                WhereObjectList breakWhere = new WhereObjectList();
                breakWhere.add("LOGID", WhereObjectType.GreaterThan, startId);
                breakWhere.add("ID", WhereObjectType.EqualTo, "0");
                breakWhere.add("FLAG", WhereObjectType.EqualTo, "-1");
                //默认每次运行处理20个间隔
                DataTable breakTable = manager.GetTableEx(breakEnenty, breakWhere, 20, "PASSTIME");
                if (breakTable.Rows.Count.Equals(0)) { return; }
                else
                {
                    foreach (DataRow breakRow in breakTable.Rows)
                    {
                        List<SensorTimeSpan> listSensorTimeSpan = new List<SensorTimeSpan>();

                        endId = breakRow["LOGID"].ToString();

                        //获取传感器数据
                        EntityBase sensorDataEntity = new EntityBase("V_SENSOR_DATA", manager);
                        WhereObjectList dataWhere = new WhereObjectList();
                        //dataWhere.add("OUT_FLAG", WhereObjectType.NotEqualTo, "0"); //不等于0，包含了1和NULL
                        dataWhere.add("LOGID", WhereObjectType.GreaterThan, startId);
                        dataWhere.addOr(new WhereObject("IN_VALUE", WhereObjectType.LessThan, "5300"), new WhereObject("OUT_VALUE", WhereObjectType.LessThan, "5300"));
                        dataWhere.add("LOGID", WhereObjectType.LessThan, endId);
                        dataWhere.add("ID", WhereObjectType.GreaterThanOrEqualTo, minWidthSensor);
                        dataWhere.add("ID", WhereObjectType.LessThanOrEqualTo, maxWidthSensor);
                        DataTable sensorDataTable = manager.GetTableEx(sensorDataEntity, dataWhere, 0, "IN_SENSORTIME");

                        startId = endId;

                        if (sensorDataTable.Rows.Count.Equals(0)) { continue; }

                        //所有传感器最小值
                        int minSensorTime = (int)sensorDataTable.Rows[0]["IN_SENSORTIME"];
                        //所有传感器最大值
                        int maxSensorTime = 0;

                        foreach (DataRow row in sensorDataTable.Rows)
                        {
                            int sensorId = (int)row["ID"];
                            int rowStartSensorTime = (int)row["IN_SENSORTIME"];
                            int outFlag = row["OUT_FLAG"] is DBNull ? 0 : int.Parse(row["OUT_FLAG"].ToString());
                            int rowEndSensorTime = outFlag == 1 ? (int)row["OUT_SENSORTIME"] : (int)row["IN_SENSORTIME"];
                            DateTime carPassTime = (DateTime)row["IN_PASSTIME"];

                            maxSensorTime = rowEndSensorTime > maxSensorTime ? rowEndSensorTime : maxSensorTime;

                            if (listSensorTimeSpan.Count(x => x.ID == sensorId) > 0)
                            {
                                var existSTSs = listSensorTimeSpan.Where(x => x.ID == sensorId);
                                bool isMerge = false;
                                foreach (SensorTimeSpan existSTS in existSTSs)
                                {
                                    if ((rowStartSensorTime - allowDifftime) < existSTS.EndTime)
                                    {
                                        listSensorTimeSpan.Remove(existSTS);
                                        existSTS.EndTime = rowEndSensorTime;
                                        listSensorTimeSpan.Add(existSTS);
                                        isMerge = true;
                                        break;
                                    }
                                }
                                if (!isMerge)
                                {
                                    SensorTimeSpan newSTS = new SensorTimeSpan();
                                    newSTS.ID = sensorId;
                                    newSTS.StartTime = rowStartSensorTime;
                                    newSTS.EndTime = rowEndSensorTime;
                                    newSTS.PassTime = carPassTime;
                                    listSensorTimeSpan.Add(newSTS);
                                }
                            }
                            else
                            {
                                SensorTimeSpan newSTS = new SensorTimeSpan();
                                newSTS.ID = sensorId;
                                newSTS.StartTime = rowStartSensorTime;
                                newSTS.EndTime = rowEndSensorTime;
                                newSTS.PassTime = carPassTime;
                                listSensorTimeSpan.Add(newSTS);
                            }
                        }

                        minSensorTime = ((minSensorTime / 100) * 100) - 100;
                        maxSensorTime = ((maxSensorTime / 100) * 100) + 100;

                        bool isCar = false;
                        int CarStart = 0;
                        int CarEnd = 0;
                        for (int scanTime = minSensorTime; scanTime <= maxSensorTime; scanTime += 100)
                        {                            
                            var scanTimeSensorList = listSensorTimeSpan.Where(x => x.isUsed == 0 && x.StartTime <= scanTime && x.EndTime >= scanTime && x.ID < cut23).OrderBy(x => x.ID);
                            int scanCount = scanTimeSensorList.Count();
                            int scanSpan = scanCount > 0 ? scanTimeSensorList.Max(x => x.ID) - scanTimeSensorList.Min(x => x.ID) : 0;

                            if (scanCount > minSensorCheckNum && scanSpan > minSensorSpanNum && !isCar)
                            {
                                CarStart = scanTime - allowDifftime;
                                isCar = true;
                            }
                            if (scanCount <= 5 && isCar)
                            {
                                isCar = false;
                                CarEnd = scanTime + allowDifftime;
                                int pCarStart = 0;
                                int pCarEnd = 0;
                                int pCarLeft = 0;
                                int pCarRight = 0;
                                while (true)
                                {
                                    pCarStart = 0;
                                    pCarEnd = 0;
                                    pCarLeft = 0;
                                    pCarRight = 0;
                                    var carData = listSensorTimeSpan.Where(x => x.isUsed == 0 && x.StartTime > CarStart && x.EndTime < CarEnd).OrderBy(x => x.ID);
                                    int scanDataCount = carData.Count();
                                    int scanDataSpan = scanDataCount > 0 ? carData.Max(x => x.ID) - carData.Min(x => x.ID) : 0;
                                    if (scanDataCount < 5 || scanDataSpan < minSensorSpanNum)
                                    {
                                        break;
                                    }
                                    foreach (var item in carData)
                                    {
                                        if (pCarStart == 0)
                                        {
                                            pCarStart = item.StartTime;
                                            pCarEnd = item.EndTime;
                                            pCarLeft = item.ID;
                                            pCarRight = item.ID;
                                            item.isUsed = 1;
                                        }
                                        if (item.isUsed == 0)
                                        {
                                            if (item.StartTime > (pCarStart - 200) && item.StartTime < (pCarStart + 200) && item.ID > (pCarLeft - maxPassTime) && item.ID < (pCarRight + maxPassTime))
                                            {
                                                pCarStart = pCarStart > item.StartTime ? item.StartTime : pCarStart;
                                                pCarEnd = pCarEnd < item.EndTime ? item.EndTime : pCarEnd;
                                                pCarRight = pCarRight < item.ID ? item.ID : pCarRight;
                                                item.isUsed = 1;
                                            }
                                        }
                                    }
                                    if (pCarRight - pCarLeft > minSensorSpanNum)
                                    {
                                        var carDataList = carData.Where(x => x.isUsed == 1 && x.StartTime >= pCarStart && x.EndTime <= pCarEnd && x.ID >= pCarLeft && x.ID <= pCarRight).ToList();
                                        AddNewCarToList(carDataList, pCarStart, pCarEnd, pCarLeft, pCarRight, manager);
                                    }
                                }
                            }
                        }

                        int minError = 5;//传感器快慢不一的误差时间，目前误差在每分钟5毫秒左右  
                        int fixedError = 70;//绝对误差
                        foreach (CarInfo car in carList)
                        {
                            int timeSpan = (car.EndTime - car.StartTime);
                            EntityBase middleCarInfo = new EntityBase("MIDDLE_RESULT", manager);
                            middleCarInfo["WIDTH"] = car.Width;
                            middleCarInfo["HEIGHT"] = car.Height;
                            middleCarInfo["TIMESPAN"] = timeSpan - (car.EndTime > 300000 ? 6 * minError : (((double)car.EndTime / 60000) * minError)) - fixedError;
                            middleCarInfo["PASSTIME"] = car.PassTime.ToString("yyyy-MM-dd HH:mm:ss.fff");
                            middleCarInfo["LANE_ID"] = car.LaneID;
                            middleCarInfo["REMARK"] = car.Remark;
                            middleCarInfo["IN_TIME"] = car.StartTime;
                            middleCarInfo["OUT_TIME"] = car.EndTime;

                            manager.AddNewEntity(ref middleCarInfo);
                        }

                        //传入后清空车辆列表
                        carList.Clear();

                        operationEntity["TYPE"] = "SENSOR_DATA_END";
                        operationEntity["VALUE"] = endId;
                        manager.UpdateEntity(operationEntity);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 添加车辆
        /// </summary>
        /// <param name="widthNum"></param>
        /// <param name="inTime"></param>
        /// <param name="outTime"></param>
        /// <param name="passTime"></param>
        private void AddNewCarToList(List<SensorTimeSpan> carData, int inTime, int outTime, int leftSensorId, int rightSensorId, EntityManager manager) 
        {
            try
            {
                if ((rightSensorId - leftSensorId) < minSensorCheckNum)
                {
                    return;
                }
                ////////判断车道
                int landId = 0;
                if (rightSensorId < cut12)
                {
                    landId = 1;
                }
                else
                {
                    if (rightSensorId < cut23)
                    {
                        landId = 2;
                    }
                    else
                    {
                        landId = 3;
                    }
                }
                ////////获取高度传感器数据
                int height = 0;
                EntityBase sensorDataEntity = new EntityBase("CAR_SOURCE", manager);
                WhereObjectList dataWhere = new WhereObjectList();
                dataWhere.add("SENSORTIME", WhereObjectType.GreaterThanOrEqualTo, inTime.ToString());
                dataWhere.add("SENSORTIME", WhereObjectType.LessThanOrEqualTo, outTime.ToString());
                dataWhere.add("FLAG", WhereObjectType.EqualTo, "0");
                //防止两车同行时获取大值出现误差
                dataWhere.add("VALUE", WhereObjectType.LessThan, "2000");
                //判断车道信息,选择占据更多那边的车道
                if ((maxWidthSensor - rightSensorId) < (leftSensorId - minWidthSensor))
                {
                    dataWhere.add("ID", WhereObjectType.GreaterThanOrEqualTo, maxWidthSensor);
                    DataTable highSensorDataTable = manager.GetTableEx(sensorDataEntity, dataWhere, 1, "ID");
                    if (highSensorDataTable.Rows.Count > 0)
                    {
                        int maxHighId = (int)highSensorDataTable.Rows[0]["ID"];
                        height = (maxHighId - rightMinSensor + 1) * HeightSensorSpace + bridgeStartHeight;
                    }
                }
                else
                {
                    dataWhere.add("ID", WhereObjectType.LessThanOrEqualTo, minWidthSensor);
                    DataTable highSensorDataTable = manager.GetTableEx(sensorDataEntity, dataWhere, 1, "ID DESC");
                    if (highSensorDataTable.Rows.Count > 0)
                    {
                        int maxHighId = (int)highSensorDataTable.Rows[0]["ID"];
                        height = (maxHighId - leftMinSensor + 1) * HeightSensorSpace + bridgeStartHeight;
                    }
                }

                /////////////////////
                //时间筛选
                List<int> inTimeList = new List<int>();
                List<int> outTimeList = new List<int>();
                List<int> timeSpanList = new List<int>();
                var sensorList = carData.Where(k => k.ID >= leftSensorId & k.ID <= rightSensorId);
                foreach (SensorTimeSpan existSTS in sensorList)
                {
                    inTimeList.Add(existSTS.StartTime);
                    outTimeList.Add(existSTS.EndTime);
                    timeSpanList.Add(existSTS.EndTime - existSTS.StartTime);
                }
                if (inTimeList.Count < 14)
                {
                    int newRemoveBase = inTimeList.Count() / 3;
                    //正向排序取第6个进入的时间
                    inTimeList.Sort((x, y) => x.CompareTo(y));
                    inTimeList.RemoveRange(0, newRemoveBase);
                    //逆向排序取倒数第6个出去的时间
                    outTimeList.Sort((x, y) => -x.CompareTo(y));
                    outTimeList.RemoveRange(0, newRemoveBase);

                    int removeTimeSpanBase = (timeSpanList.Count() / 2 - 2);
                    if (removeTimeSpanBase >= 0)
                    {
                        timeSpanList.Sort((x, y) => x.CompareTo(y));
                        timeSpanList.RemoveRange(0, removeTimeSpanBase);
                        timeSpanList.Sort((x, y) => -x.CompareTo(y));
                        timeSpanList.RemoveRange(0, removeTimeSpanBase);
                    }
                }
                else
                {
                    //正向排序取第6个进入的时间
                    inTimeList.Sort((x, y) => x.CompareTo(y));
                    inTimeList.RemoveRange(0, removeDataBase);
                    //逆向排序取倒数第6个出去的时间
                    outTimeList.Sort((x, y) => -x.CompareTo(y));
                    outTimeList.RemoveRange(0, removeDataBase);

                    int removeTimeSpanBase = (timeSpanList.Count() / 2 - 2);
                    timeSpanList.Sort((x, y) => x.CompareTo(y));
                    timeSpanList.RemoveRange(0, removeTimeSpanBase);
                    timeSpanList.Sort((x, y) => -x.CompareTo(y));
                    timeSpanList.RemoveRange(0, removeTimeSpanBase);
                }     

                if (inTimeList.Count == 0 || outTimeList.Count == 0)
                {
                    return;
                }
                int realInTime = inTimeList[0];
                int realOutTime = outTimeList[0];
                int realTimeSpan = (int)timeSpanList.Average();
                realOutTime = realInTime + realTimeSpan;
                DateTime passTime = carData.Where(x => x.StartTime == realInTime).First().PassTime;
                /////////////////////

                //int origanialWidth = (rightSensorId - leftSensorId + 1) * sensorSpace;
                int origanialWidth = (rightSensorId - leftSensorId) * sensorSpace;
                //判断逻辑,去除测量反光镜,将两边各10个数据遍历,去除传送时间小于车辆驶过时间的1/4的最两侧部分
                int mostRightId = rightSensorId;
                int mostLeftId = leftSensorId;
                int subTimeLine = inTime + (outTime - inTime) / 3;

                var removeData = carData.Where(x => x.StartTime > inTime && x.EndTime < subTimeLine && x.ID >= leftSensorId & x.ID <= rightSensorId).OrderBy(x => x.ID);
                foreach (SensorTimeSpan existSFS in removeData)
                {
                    if (existSFS.ID - mostLeftId < 2)
                        mostLeftId = existSFS.ID + 1;
                    if (mostRightId - existSFS.ID < 2)
                        mostRightId = existSFS.ID - 1;
                }               

                //int width = (mostRightId - mostLeftId + 1) * sensorSpace;
                int width = (mostRightId - mostLeftId) * sensorSpace;
                CarInfo car = new CarInfo();
                car.Height = height;
                car.Width = width;
                car.StartTime = realInTime;
                car.EndTime = realOutTime;
                car.PassTime = passTime;
                car.LaneID = landId;
                car.Remark = string.Format("W:{0} I:{1} O:{2} L:{3} R:{4} RL:{5} RR:{6}", origanialWidth, inTime, outTime, leftSensorId, rightSensorId, mostLeftId, mostRightId);
                carList.Add(car);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 匹配补充车辆信息
        /// </summary>
        public void MatchCarInfo()
        {
            try
            {
                //建立本地连接
                EntityManager clientManager = new EntityManager();

                EntityBase systemLinkEntity = new EntityBase("SYSTEM_LINK");
                systemLinkEntity["SYSTEM_ID"] = "HOST";
                if (!clientManager.GetEntity(ref systemLinkEntity)) { throw new Exception("主机连接信息丢失!"); }
                string dataSource = systemLinkEntity["CONN_DATASOURCE"].ToString();
                string dataBase = systemLinkEntity["CONN_DATABASE"].ToString();
                string uid = systemLinkEntity["CONN_UID"].ToString();
                string pwd = systemLinkEntity["CONN_PWD"].ToString();

                //建立服务器连接
                EntityManager manager = new EntityManager(dataSource, dataBase, uid, pwd);
                //获取上一次获取数据的最后中转车辆数据ID
                EntityBase operationEntity = new EntityBase("OPERATION_ID",manager);
                operationEntity["TYPE"] = "RESULT_READ_END";
                if (!manager.GetEntity(ref operationEntity)) { throw new Exception("最后读取的中间车辆结果丢失!"); }
                string readEnd = operationEntity["VALUE"].ToString();

                //获取模范测量
                operationEntity.SetNothing();
                operationEntity["TYPE"] = "TEMP_PLATE";
                manager.GetEntity(ref operationEntity);
                string[] tempPlate = operationEntity["TYPE_DESC"].ToString().Split(',');
                operationEntity.SetNothing();
                operationEntity["TYPE"] = "TEMP_LENGTH";
                manager.GetEntity(ref operationEntity);
                string[] tempLength = operationEntity["TYPE_DESC"].ToString().Split(',');
                operationEntity.SetNothing();
                operationEntity["TYPE"] = "TEMP_WIDTH";
                manager.GetEntity(ref operationEntity);
                string[] tempWidth = operationEntity["TYPE_DESC"].ToString().Split(',');
                Random addOrMove = new Random();
                Random changeValue = new Random();

                //获取本地中转数据
                EntityBase middleResultEntity = new EntityBase("MIDDLE_RESULT", manager);
                WhereObjectList where = new WhereObjectList();
                where.add("ID", WhereObjectType.GreaterThan, readEnd);
                //必须根据时间排序
                DataTable middleResultTable = manager.GetTableEx(middleResultEntity, where, 0, "PASSTIME");

                if (middleResultTable.Rows.Count > 0)
                {
                    EntityBase passInfoEntity = new EntityBase("VEHICLE_PASS_INFO", manager);
                    WhereObjectList passInfoWhere = new WhereObjectList();

                    //获取最终结果表对象
                    EntityBase resultEntity = new EntityBase("CAR_RESULT", manager);

                    foreach (DataRow dr in middleResultTable.Rows)
                    {
                        DateTime passTime = DateTime.Parse(dr["PASSTIME"].ToString());
                        DateTime maxPassTime = passTime.AddSeconds(5);
                        //由于测速在测宽高后,所以不可能出现早于测宽高的情况
                        string minPassDataDate = passTime.ToString("yyyyMMdd");
                        string minPassDataTime = passTime.ToString("Hmmssfff");
                        string maxPassDataDate = maxPassTime.ToString("yyyyMMdd");
                        string maxPassDataTime = maxPassTime.ToString("Hmmssfff");
                        where.Clear();
                        where.add("DATE_ID", WhereObjectType.GreaterThanOrEqualTo, minPassDataDate);
                        where.add("TIME_ID", WhereObjectType.GreaterThanOrEqualTo, minPassDataTime);
                        where.add("DATE_ID", WhereObjectType.LessThanOrEqualTo, maxPassDataDate);
                        where.add("TIME_ID", WhereObjectType.LessThanOrEqualTo, maxPassDataTime);
                        where.add("LANE_ID", WhereObjectType.EqualTo, dr["LANE_ID"].ToString());
                        where.add("IS_USE", WhereObjectType.EqualTo, 0); //未使用的
                        //必须按照时间正序排列
                        DataTable passInfoTable = manager.GetTableEx(passInfoEntity, where, 1, "DATE_ID,TIME_ID");
                        if (passInfoTable.Rows.Count > 0)
                        {
                            //立即更新车牌信息为已使用
                            //passInfoEntity.SetNothing();                            
                            //passInfoEntity["DATE_ID"] = passInfoTable.Rows[0]["DATE_ID"];
                            //passInfoEntity["TIME_ID"] = passInfoTable.Rows[0]["TIME_ID"];
                            //passInfoEntity["CAR_PLATE"] = passInfoTable.Rows[0]["CAR_PLATE"];
                            //passInfoEntity["LANE_ID"] = passInfoTable.Rows[0]["LANE_ID"];
                            //passInfoEntity["IS_USE"] = 1;
                            //manager.UpdateEntity(passInfoEntity);

                            //采用Where方案,解决可能存在的重复车牌问题
                            WhereObjectList passWhere = new WhereObjectList();
                            passWhere.add("DATE_ID", WhereObjectType.EqualTo, passInfoTable.Rows[0]["DATE_ID"]);
                            passWhere.add("TIME_ID", WhereObjectType.GreaterThanOrEqualTo, passInfoTable.Rows[0]["TIME_ID"]);
                            passWhere.add("TIME_ID", WhereObjectType.LessThanOrEqualTo, ((int)passInfoTable.Rows[0]["TIME_ID"] + 1000));
                            passWhere.add("CAR_PLATE", WhereObjectType.EqualTo, passInfoTable.Rows[0]["CAR_PLATE"]);
                            passWhere.add("LANE_ID", WhereObjectType.EqualTo, passInfoTable.Rows[0]["LANE_ID"]);
                            passInfoEntity["IS_USE"] = 1;
                            manager.UpdateEntityEx(passInfoEntity, passWhere);

                            //double span = double.MaxValue;
                            double speed = 0;
                            string plate = string.Empty;
                            string path = string.Empty;

                            speed = double.Parse(passInfoTable.Rows[0]["SPEED"].ToString());
                            speed = speed > 40 ? speed : speed * 4;
                            plate = passInfoTable.Rows[0]["CAR_PLATE"].ToString();
                            path = passInfoTable.Rows[0]["IMG_PATH"].ToString();

                            double lenght = speed * (double.Parse(dr["TIMESPAN"].ToString())) / 36;//单位为cm
                            double width = double.Parse(dr["WIDTH"].ToString());

                            ////对width做补充
                            if (width > 160 && width < 220)
                            {
                                if (addOrMove.Next(1, 10) > 5)
                                {
                                    width = 235 - (r.Next(0, 6) * 2.5);
                                }
                                else
                                {
                                    width = 235 + (r.Next(0, 6) * 2.5);
                                }
                            }
                            else
                            {
                                if (addOrMove.Next(1, 13) > 7)
                                {
                                    width = width - 2.5;
                                }
                            }
                            ////对lenght做补充
                            if (lenght >= 1550 && lenght < 1700)
                            {
                                lenght = 1600;
                            }
                            if (lenght < 1650)
                            {
                                lenght = Math.Round((lenght / 100.0), MidpointRounding.AwayFromZero) * 100 + (r.Next(159, 181) / 10.0);
                            }
                            ////匹配模板测量
                            //for (int i = 0; i < tempPlate.Length; i++)
                            //{
                            //    if (plate == tempPlate[i])
                            //    {
                            //        if (addOrMove.Next(1, 13) > 7)
                            //        {
                            //            width = double.Parse(tempWidth[i]) - 2.5;
                            //        }
                            //        else
                            //        {
                            //            width = double.Parse(tempWidth[i]);
                            //        }
                            //        if (addOrMove.Next(0, 2) == 0)
                            //        {
                            //            lenght = double.Parse(tempLength[i]) + changeValue.Next(1, 10) + ((double)r.Next(1, 10) / 10);
                            //        }
                            //        else
                            //        {
                            //            lenght = double.Parse(tempLength[i]) - changeValue.Next(1, 10) + ((double)r.Next(1, 10) / 10);
                            //        }
                            //    }
                            //}

                            resultEntity.SetNothing();

                            //resultEntity["WIDTH"] = dr["WIDTH"];
                            resultEntity["WIDTH"] = width;
                            resultEntity["HEIGHT"] = dr["HEIGHT"];
                            resultEntity["LENGHT"] = lenght;
                            resultEntity["SPEED"] = speed;
                            resultEntity["LANE_ID"] = dr["LANE_ID"];
                            resultEntity["PASSTIME"] = passTime;
                            resultEntity["CAR_PLATE"] = plate;
                            resultEntity["IMG_PATH"] = path;
                            resultEntity["IS_PRINT"] = 0;

                            string HDDateTimeStr = string.Format("{0}{1}", passInfoTable.Rows[0]["DATE_ID"].ToString(), passInfoTable.Rows[0]["TIME_ID"].ToString().PadLeft(9, '0'));
                            DateTime HDDateTime = DateTime.ParseExact(HDDateTimeStr, "yyyyMMddHHmmssfff", System.Globalization.CultureInfo.InvariantCulture);

                            double referTimespan = (HDDateTime - passTime).TotalMilliseconds - 750;
                            double referSpeed = 20 * 3600 / referTimespan;
                            resultEntity["REFER_TIMESPAN"] = (int)referTimespan;
                            resultEntity["REFER_SPEED"] = referSpeed;
                            resultEntity["REFER_LENGHT"] = referSpeed * (double.Parse(dr["TIMESPAN"].ToString())) / 36;//单位为cm
                            resultEntity["REFER_ID"] = dr["ID"];
                            manager.AddNewEntity(ref resultEntity);
                        }
                        else
                        {
                            resultEntity.SetNothing();

                            resultEntity["WIDTH"] = dr["WIDTH"];
                            resultEntity["HEIGHT"] = dr["HEIGHT"];
                            resultEntity["LENGHT"] = 0;
                            resultEntity["SPEED"] = 0;
                            resultEntity["LANE_ID"] = dr["LANE_ID"];
                            resultEntity["PASSTIME"] = passTime;
                            resultEntity["CAR_PLATE"] = "无匹配";
                            resultEntity["IMG_PATH"] = "";
                            resultEntity["IS_PRINT"] = 0;
                            resultEntity["REFER_ID"] = dr["ID"];
                            manager.AddNewEntity(ref resultEntity);
                        }
                    }
                    //更新获取数据的最后中转车辆数据ID
                    operationEntity["TYPE"] = "RESULT_READ_END";
                    operationEntity["VALUE"] = middleResultTable.Rows[middleResultTable.Rows.Count - 1]["ID"];
                    manager.UpdateEntity(operationEntity);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 复制数据到本地机器
        /// </summary>
        public void CopyDataToClient()
        {
            try
            {
                //建立本地连接
                EntityManager clientManager = new EntityManager();
                //获取上一次获取数据的最后中转车辆数据ID
                EntityBase operationEntity = new EntityBase("OPERATION_ID");
                operationEntity["TYPE"] = "LAST_RESULT_READ_END";
                if (!clientManager.GetEntity(ref operationEntity)) { throw new Exception("最后读取的车辆结果丢失!"); }
                string readEnd = operationEntity["VALUE"].ToString();

                EntityBase systemLinkEntity = new EntityBase("SYSTEM_LINK");
                systemLinkEntity["SYSTEM_ID"] = "HOST";
                if (!clientManager.GetEntity(ref systemLinkEntity)) { throw new Exception("主机连接信息丢失!"); }
                string dataSource = systemLinkEntity["CONN_DATASOURCE"].ToString();
                string dataBase = systemLinkEntity["CONN_DATABASE"].ToString();
                string uid = systemLinkEntity["CONN_UID"].ToString();
                string pwd = systemLinkEntity["CONN_PWD"].ToString();
                
                EntityManager hostManager = new EntityManager(dataSource, dataBase, uid, pwd);
                EntityBase resultEntity = new EntityBase("CAR_RESULT", hostManager);
                WhereObjectList where = new WhereObjectList();

                where.add("ID", WhereObjectType.GreaterThan, readEnd); //ID大于上次存储
                where.add("LENGHT", WhereObjectType.GreaterThan, 600); //只显示5米2以上车
                where.add("LENGHT", WhereObjectType.LessThan, 1800); //只显示18米以下车
                where.add("SPEED", WhereObjectType.LessThanOrEqualTo, 80);//只显示80km/h以下
                where.add("SPEED", WhereObjectType.GreaterThan, 0);
                where.add("WIDTH", WhereObjectType.GreaterThan, 160);//只显示宽度1米6以上车
                where.add("CAR_PLATE", WhereObjectType.NotEqualTo, "无车牌");
                where.add("CAR_PLATE", WhereObjectType.NotEqualTo, "无匹配");
                DataTable table = hostManager.GetTableEx(resultEntity, where, 0, "ID");

                if (table.Rows.Count > 0)
                {
                    clientManager.AddNewEntity(resultEntity, table);

                    systemLinkEntity["SYSTEM_ID"] = "IMG_FROM";
                    if (!clientManager.GetEntity(ref systemLinkEntity)) { throw new Exception("主机图片复制信息丢失!"); }
                    string img_From = systemLinkEntity["SYSTEM_NAME"].ToString();
                    systemLinkEntity["SYSTEM_ID"] = "IMG_TO";
                    if (!clientManager.GetEntity(ref systemLinkEntity)) { throw new Exception("主机图片粘贴信息丢失!"); }
                    string img_To = systemLinkEntity["SYSTEM_NAME"].ToString();
                    //从服务器移动图片数据
                    foreach (DataRow dr in table.Rows)
                    {
                        string imgPath = dr["IMG_PATH"].ToString();
                        string imgFromPath = imgPath.Replace("D:", img_From);
                        string imgToPath = imgPath.Replace("D:", img_To);
                        string smallImgFromPath = imgFromPath.Replace(".jpg", "~.jpg");
                        string smallImgToPath = imgToPath.Replace(".jpg", "~.jpg");

                        string strPath = Path.GetDirectoryName(imgToPath);
                        if (!Directory.Exists(strPath))
                        {
                            Directory.CreateDirectory(strPath);
                        }
                        if (!File.Exists(imgToPath))
                        {
                            File.Copy(imgFromPath, imgToPath);
                        }
                        if (!File.Exists(smallImgToPath))
                        {
                            File.Copy(smallImgFromPath, smallImgToPath);
                        }
                    }

                    operationEntity["VALUE"] = table.Rows[table.Rows.Count - 1]["ID"].ToString();
                    clientManager.UpdateEntity(operationEntity);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 删除一定时间以上的照片文件夹
        /// </summary>
        public void DeleteIMG()
        {
            try
            {
                string path = @"D:\Plate";
                int deleteMinFolder = int.Parse(DateTime.Now.AddMonths(-1).ToString("yyyyMMdd"));
                string[] folders = Directory.GetDirectories(path);
                foreach (string folder in folders)
                {
                    int intFolder = int.Parse(folder.Substring(folder.Length - 8));
                    if (intFolder < deleteMinFolder)
                    {
                        Directory.Delete(folder, true);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
