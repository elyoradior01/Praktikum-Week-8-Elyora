using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Praktikum_Week_8_Elyora
{
    public partial class FormTanding : Form
    {
        public FormTanding()
        {
            InitializeComponent();
        }

        public static string sqlconnection = "server=localhost;uid=root;pwd=;database=premier_league";
        public MySqlConnection sqlConnect = new MySqlConnection(sqlconnection);
        public MySqlCommand sqlCommand;
        public MySqlDataAdapter sqlAdapter;
        string sqlQuery;

        private void Form1_Load(object sender, EventArgs e)
        {
            DataTable dtTeam = new DataTable();
            sqlQuery = "SELECT team_name as `Nama Tim`, manager_name as `Nama Man`, p.player_name, t.team_id as `ID TEAM` FROM team t, manager m, player p WHERE m.manager_id = t.manager_id and t.captain_id = p.player_id";
            sqlCommand = new MySqlCommand(sqlQuery, sqlConnect);
            sqlAdapter = new MySqlDataAdapter(sqlCommand);
            sqlAdapter.Fill(dtTeam);
            DataTable dtLawan = new DataTable();
            sqlQuery = "SELECT team_name as `Nama Tim`, manager_name as `Nama Man`, p.player_name FROM team t, manager m, player p WHERE m.manager_id = t.manager_id and t.captain_id = p.player_id";
            sqlCommand = new MySqlCommand(sqlQuery, sqlConnect);
            sqlAdapter = new MySqlDataAdapter(sqlCommand);
            sqlAdapter.Fill(dtLawan);
            cbHome.DataSource = dtTeam;
            cbHome.DisplayMember = "Nama Tim";
            cbHome.ValueMember = "Nama Tim";
            cbLawan.DataSource = dtLawan;
            cbLawan.DisplayMember = "Nama Tim";
            cbLawan.ValueMember = "Nama Tim";


        }

        private void cbHome_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                DataTable timhome = new DataTable();
                sqlQuery = "SELECT t.team_name as `Nama Tim`, m.manager_name as `Nama Man`, p.player_name, t.team_id as `ID TEAM` , t.capacity, concat(t.home_stadium, ',', t.city) "+
                    "FROM team t, manager m, player p WHERE m.manager_id = t.manager_id and t.captain_id = p.player_id And t.team_name = '"+cbHome.SelectedValue.ToString()+"'";
                sqlCommand = new MySqlCommand(sqlQuery, sqlConnect);
                sqlAdapter = new MySqlDataAdapter(sqlCommand);
                sqlAdapter.Fill(timhome);

                lblManagerHome.Text = timhome.Rows[0][1].ToString();
                lblCaptainHome.Text = timhome.Rows[0][2].ToString();
                lblStadium.Text = timhome.Rows[0][5].ToString();
                lblCapacity.Text = timhome.Rows[0][4].ToString();
            }
            catch (Exception)
            {

                
            }


        }

        private void cbLawan_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable timlawan = new DataTable();
                sqlQuery = "SELECT t.team_name as `Nama Tim`, m.manager_name as `Nama Man`, p.player_name, t.team_id as `ID TEAM` FROM team t, manager m, player p WHERE m.manager_id = t.manager_id and t.captain_id = p.player_id And t.team_name = '" + cbLawan.SelectedValue.ToString() + "'";
                sqlCommand = new MySqlCommand(sqlQuery, sqlConnect);
                sqlAdapter = new MySqlDataAdapter(sqlCommand);
                sqlAdapter.Fill(timlawan);
                
                lblManagerLawan.Text = timlawan.Rows[0][1].ToString();
                lblCaptainLawan.Text = timlawan.Rows[0][2].ToString();


            }
            catch (Exception)
            {


            }



        }
    }
}
