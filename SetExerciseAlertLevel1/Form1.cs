using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dapper;
using System.Data.SqlClient;
namespace SetExerciseAlertLevel1
{
    public partial class Form1 : Form
    {
        public string currentScope;
        public string currentAirWarn;
        public string currentDefCon;



        static SqlConnection sqlCon = new SqlConnection(ConnectionString.GetConnectionString());
        public Form1()
        {
            InitializeComponent();

        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            FillDataGridView_CurrentData();
            FillDataGridView_HistoryData();
            try
            {
                fadeoutRadio.Checked = true;
                var checkCurrentScope = sqlCon.ExecuteScalar("select Scope from Set_Exercise_Alert_Level");
                currentScope = Convert.ToString(checkCurrentScope);
                var checkCurrentAirWarn = sqlCon.ExecuteScalar("select AirDefenseWarning from Set_Exercise_Alert_Level");
                currentAirWarn = Convert.ToString(checkCurrentAirWarn);
                var checkCurrentDefCon = sqlCon.ExecuteScalar("select DefConLevel from Set_Exercise_Alert_level");
                currentDefCon = Convert.ToString(checkCurrentDefCon);
                Data.CurrentScope = currentScope;
                Data.CurrentAirWarn = currentAirWarn;
                Data.CurrentDefCon = currentDefCon;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            Invoke();
        }


        void Scope_Check_Process()
        {
            if (radio1.Checked == true)
            {
                Data.Scope = radio1.Text;
            }else if (radio2.Checked == true)
            {
                Data.Scope = radio2.Text;
            }else if(radio1.Checked && radio2.Checked == false)
            {
                MessageBox.Show("Empty");
            }
        }

        void DefCon_Check_Process()
        {
            if(fadeoutRadio.Checked == true)
            {
                Data.DefCon = fadeoutRadio.Text;
            }
            else if (doubletakeRadio.Checked == true)
            {
                Data.DefCon = doubletakeRadio.Text;
            }
            else if (roundhouseRadio.Checked == true)
            {
                Data.DefCon = roundhouseRadio.Text;
            }
            else if (fastpaceRadio.Checked == true)
            {
                Data.DefCon = fastpaceRadio.Text;
            }
            else if (cockedpistolRadio.Checked == true)
            {
                Data.DefCon = cockedpistolRadio.Text;
            }
            else if (bignoiseRadio.Checked == true)
            {
                Data.DefCon = bignoiseRadio.Text;
            }
        }

        void AirDefenseWarning_Check_Process()
        {
            if(snowmanRadio.Checked == true)
            {
                Data.AirDefenseWarning = snowmanRadio.Text;
            }
            else if (lemonjuiceRadio.Checked == true)
            {
                Data.AirDefenseWarning = lemonjuiceRadio.Text;
            }
            else if (applejackRadio.Checked == true)
            {
                Data.AirDefenseWarning = applejackRadio.Text;
            }
        }

        void Add_Data()
        {
            if (Data.Scope == null || Data.DefCon == null)
            {
                MessageBox.Show("Plese select Scope and Defcon");
            }
            else
            {
                try
                {
                    DynamicParameters param = new DynamicParameters();
                    param.Add("@Scope", Data.Scope);
                    param.Add("@DefConLevel", Data.DefCon);
                    param.Add("@AirDefenseWarning", Data.AirDefenseWarning);
                    param.Add("@TimeDate", DateTime.Now);
                    sqlCon.Execute("Proc_Set_Exercise_Alert_Level", param, commandType: CommandType.StoredProcedure);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    MessageBox.Show("Added Successfully");
                }
            }


        }

        void FillDataGridView_CurrentData()
        {
            var data = sqlCon.Query<CurrentDataMapping>($"select * from Set_Exercise_Alert_Level").ToList<CurrentDataMapping>();
            dataGridView1.DataSource = data;
        }

        void FillDataGridView_HistoryData()
        {
            var data = sqlCon.Query<HistoryDataMapping>($"select * from Set_Exercise_Alert_Level_History").ToList<HistoryDataMapping>();
            dataGridView2.DataSource = data;
        }

        void SystemCheck(string scope , string airwarn)
        {
            if (scope == "System")
            {
                if (airwarn == "Apple Jack")
                {
                    snowmanRadio.Enabled = false;
                    lemonjuiceRadio.Enabled = false;
                    applejackRadio.Enabled = true;
                }
                else if(airwarn == "Lemon Juice")
                {
                    snowmanRadio.Enabled = false;
                    lemonjuiceRadio.Enabled = true;
                    applejackRadio.Enabled = true;
                }else if(airwarn == "Snow Man")
                {
                    snowmanRadio.Enabled = true;
                    lemonjuiceRadio.Enabled = true;
                    applejackRadio.Enabled = true;
                }
            }
            else if (scope == "Local")
            {
                snowmanRadio.Enabled = true;
                lemonjuiceRadio.Enabled = true;
                applejackRadio.Enabled = true;
            }
        }
        void DefConChecked(string scope, string defCon)
        {
            if (scope == "System")
            {
                if (defCon == "Big Noise")
                {
                    fadeoutRadio.Enabled = false;
                    doubletakeRadio.Enabled = false;
                    roundhouseRadio.Enabled = false;
                    fastpaceRadio.Enabled = false;
                    cockedpistolRadio.Enabled = false;
                    bignoiseRadio.Enabled = true;
                }
                else if (defCon == "Cocked Pistol")
                {
                    fadeoutRadio.Enabled = false;
                    doubletakeRadio.Enabled = false;
                    roundhouseRadio.Enabled = false;
                    fastpaceRadio.Enabled = false;
                    cockedpistolRadio.Enabled = true;
                    bignoiseRadio.Enabled = true;
                }
                else if (defCon == "Fastpace")
                {
                    fadeoutRadio.Enabled = false;
                    doubletakeRadio.Enabled = false;
                    roundhouseRadio.Enabled = false;
                    fastpaceRadio.Enabled = true;
                    cockedpistolRadio.Enabled = true;
                    bignoiseRadio.Enabled = true;
                }
                else if(defCon == "Roundhouse")
                {
                    fadeoutRadio.Enabled = false;
                    doubletakeRadio.Enabled = false;
                    roundhouseRadio.Enabled = true;
                    fastpaceRadio.Enabled = true;
                    cockedpistolRadio.Enabled = true;
                    bignoiseRadio.Enabled = true;
                }
                else if(defCon == "DoubleTake")
                {
                    fadeoutRadio.Enabled = false;
                    doubletakeRadio.Enabled = true;
                    roundhouseRadio.Enabled = true;
                    fastpaceRadio.Enabled = true;
                    cockedpistolRadio.Enabled = true;
                    bignoiseRadio.Enabled = true;
                }else if(defCon == "Fadeout")
                {
                    fadeoutRadio.Enabled = true;
                    doubletakeRadio.Enabled = true;
                    roundhouseRadio.Enabled = true;
                    fastpaceRadio.Enabled = true;
                    cockedpistolRadio.Enabled = true;
                    bignoiseRadio.Enabled = true;
                }

            }else if(scope == "Local")
            {
                fadeoutRadio.Enabled = true;
                doubletakeRadio.Enabled = true;
                roundhouseRadio.Enabled = true;
                fastpaceRadio.Enabled = true;
                cockedpistolRadio.Enabled = true;
                bignoiseRadio.Enabled = true;
            }
        }

        private void radio1_CheckedChanged(object sender, EventArgs e)
        {
            snowmanRadio.Checked = false;
            lemonjuiceRadio.Checked = false;
            applejackRadio.Checked = false;

            fadeoutRadio.Checked = false;
            doubletakeRadio.Checked = false;
            roundhouseRadio.Checked = false;
            fastpaceRadio.Checked = false;
            cockedpistolRadio.Checked = false;
            bignoiseRadio.Checked = false;

            DefConChecked(Data.CurrentScope, Data.CurrentDefCon);
            SystemCheck(Data.CurrentScope, Data.CurrentAirWarn);
            
        }

        private void radio2_CheckedChanged(object sender, EventArgs e)
        {
            snowmanRadio.Enabled = true;
            lemonjuiceRadio.Enabled = true;
            applejackRadio.Enabled = true;

            fadeoutRadio.Enabled = true;
            doubletakeRadio.Enabled = true ;
            roundhouseRadio.Enabled = true;
            fastpaceRadio.Enabled = true;
            cockedpistolRadio.Enabled = true;
            bignoiseRadio.Enabled = true;
        }

        void SelectThenAdd()
        {
            try
            {
                var scopeTemp = sqlCon.ExecuteScalar("select Scope from Set_Exercise_Alert_Level");
                Data.ScopeTemp = Convert.ToString(scopeTemp);
                var defConLevelTemp = sqlCon.ExecuteScalar("select DefConLevel from Set_Exercise_Alert_Level");
                Data.DefConTemp = Convert.ToString(defConLevelTemp);
                var airWarningTemp = sqlCon.ExecuteScalar("select AirDefenseWarning from Set_Exercise_Alert_Level");
                Data.AirDefenseWarningTemp = Convert.ToString(airWarningTemp);
                var timeDataTemp = sqlCon.ExecuteScalar("select TimeDate from Set_Exercise_Alert_Level");
                Data.TimeDataTemp = Convert.ToString(timeDataTemp);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void AddThenDelete()
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@Scope", Data.ScopeTemp);
                param.Add("@DefConLevel", Data.DefConTemp);
                param.Add("@AirDefenseWarning", Data.AirDefenseWarningTemp);
                param.Add("@TimeDate", Data.TimeDataTemp);
                sqlCon.Execute("Proc_Set_Exercise_Alert_Level_History", param, commandType: CommandType.StoredProcedure);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
        void DeleteOldData()
        {
            try
            {
                sqlCon.Execute("delete from Set_Exercise_Alert_Level");
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }



        void Invoke()
        {
            Scope_Check_Process();
            DefCon_Check_Process();
            AirDefenseWarning_Check_Process();

            int checkIsNull = sqlCon.ExecuteScalar<int>("select count(TimeDate) from Set_Exercise_Alert_Level");
            if (checkIsNull == 0)
            {
                Add_Data();
            }
            else
            {
                SelectThenAdd();
                AddThenDelete();
                DeleteOldData();
                Add_Data();
            }

        }
    }
}
