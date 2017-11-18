using System;
using System.Windows.Forms;
using DatabaseProcessing;

namespace ViewDataAndPrint
{
    public partial class ChangeValue : Form
    {
        EntityManager manager;
        PrePrint pp;
        string ID;
        string Type;
        public ChangeValue(PrePrint _PrePrint, EntityManager _entityManager, string _ID, string _Type) 
        {
            pp = _PrePrint;
            manager = _entityManager;
            ID = _ID;
            Type = _Type;
            InitializeComponent();
        }

        private void ChangeValue_Load(object sender, EventArgs e)
        {
            try
            {
                switch (Type)
                {
                    case "WIDTH":
                        this.Text = "车宽(M)";
                        lblType.Text = "修改 车宽(M):";
                        break;
                    case "HEIGHT":
                        this.Text = "车高(M)";
                        lblType.Text = "修改 车高(M):";
                        break;
                    case "LENGHT":
                        this.Text = "车长(M)";
                        lblType.Text = "修改 车长(M):";
                        break;
                    case "SPEED":
                        this.Text = "车速(KM/H)";
                        lblType.Text = "修改 车速(KM/H):";
                        break;
                    case "CAR_PLATE":
                        this.Text = "车牌";
                        lblType.Text = "修改 车牌:";
                        break;
                    default:
                        throw new Exception("无该类型!");
                }
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                double doubleValue;
                string strValue;
                switch (Type)
                {
                    case "WIDTH":
                        if (!double.TryParse(txtValue.Text, out doubleValue)) { throw new Exception("请输入正确的数值!"); }
                        doubleValue *= 100;
                        strValue = doubleValue.ToString("0.000");
                        break;
                    case "HEIGHT":
                        if (!double.TryParse(txtValue.Text, out doubleValue)) { throw new Exception("请输入正确的数值!"); }
                        doubleValue *= 100;
                        strValue = doubleValue.ToString("0.000");
                        break;
                    case "LENGHT":
                        if (!double.TryParse(txtValue.Text, out doubleValue)) { throw new Exception("请输入正确的数值!"); }
                        doubleValue *= 100;
                        strValue = doubleValue.ToString("0.000");
                        break;
                    case "SPEED":
                        if (!double.TryParse(txtValue.Text, out doubleValue)) { throw new Exception("请输入正确的数值!"); }
                        doubleValue *= 1;
                        strValue = doubleValue.ToString("0.000");
                        break;
                    case "CAR_PLATE":
                        strValue = txtValue.Text;
                        break;
                    default:
                        throw new Exception("无该类型!");
                }
                EntityBase resultEntity = new EntityBase("CAR_RESULT");
                resultEntity["ID"] = ID;
                resultEntity[Type] = strValue;
                manager.UpdateEntity(resultEntity);
                pp.RefreshData();
                pp.main.RefreshData();
                MessageBox.Show(string.Format("修改 {0} 成功!", this.Text));
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
