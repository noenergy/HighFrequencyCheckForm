using System;
using System.Reflection;
using System.Windows.Forms;

namespace TaskProcess
{
    class TaskProcess
    {
        static void Main(string[] args)
        {
            try
            {
                if (args[0].Equals("0"))
                {
                    string dllName = args[1];
                    string nameSpace_Class = args[2];
                    string functionName = args[3];
                    int paraNum = int.Parse(args[4]);
                    if (paraNum > 7) { throw new Exception("支持参数数量最大为7!"); }
                    object[] para = null;
                    if (!paraNum.Equals(0))
                    {
                        para = new object[paraNum];
                        for (int i = 0; i < paraNum; i++)
                        {
                            para[i] = args[(i + 5)];
                        }
                    }

                    Assembly ass = Assembly.LoadFrom(dllName);
                    Type t = ass.GetType(nameSpace_Class);
                    object o = Activator.CreateInstance(t);
                    MethodInfo func = t.GetMethod(functionName);
                    if (func.ReturnType.Name.Equals("Void"))
                    {
                        func.Invoke(o, para);
                    }
                    else
                    {
                        string ret = func.Invoke(o, para).ToString();
                        Console.Write(ret);
                    }
                }
                else
                {
                    string dllName = args[1];
                    string nameSpace_Class = args[2];
                    Assembly assform = Assembly.LoadFrom(dllName);
                    Type tform = assform.GetType(nameSpace_Class);
                    Form form = Activator.CreateInstance(tform) as Form;
                    Form tempForm = new Form();
                    form.ShowDialog(tempForm);
                    tempForm.Dispose(); 
                }  
            }
            catch (Exception ex)
            {
                Console.Write("ERROR:" + ex.Message);
            }
        }
    }
}
