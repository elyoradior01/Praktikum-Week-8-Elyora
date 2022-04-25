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
            sqlQuery = "SELECT team_name as `Nama Tim`, t.team_id as `ID Tim`,manager_name as `Nama Man`, p.player_name, t.team_id as `ID TEAM` FROM team t, manager m, player p WHERE m.manager_id = t.manager_id and t.captain_id = p.player_id";
            sqlCommand = new MySqlCommand(sqlQuery, sqlConnect);
            sqlAdapter = new MySqlDataAdapter(sqlCommand);
            sqlAdapter.Fill(dtTeam);
            DataTable dtLawan = new DataTable();
            sqlQuery = "SELECT team_name as `Nama Tim`,t.team_id as `ID Tim`, manager_name as `Nama Man`, p.player_name FROM team t, manager m, player p WHERE m.manager_id = t.manager_id and t.captain_id = p.player_id";
            sqlCommand = new MySqlCommand(sqlQuery, sqlConnect);
            sqlAdapter = new MySqlDataAdapter(sqlCommand);
            sqlAdapter.Fill(dtLawan);
            cbHome.DataSource = dtTeam;
            cbHome.DisplayMember = "Nama Tim";
            cbHome.ValueMember = "ID Tim";
            cbLawan.DataSource = dtLawan;
            cbLawan.DisplayMember = "Nama Tim";
            cbLawan.ValueMember = "ID Tim";


        }

        private void cbHome_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                DataTable timhome = new DataTable();
                sqlQuery = "SELECT t.team_name as `Nama Tim`, m.manager_name as `Nama Man`, p.player_name, t.team_id as `ID TEAM` , t.capacity, concat(t.home_stadium, ',', t.city) "+
                    "FROM team t, manager m, player p WHERE m.manager_id = t.manager_id and t.captain_id = p.player_id And t.team_id = '"+cbHome.SelectedValue.ToString()+"'";
                sqlCommand = new MySqlCommand(sqlQuery, sqlConnect);
                sqlAdapter = new MySqlDataAdapter(sqlCommand);
                sqlAdapter.Fill(timhome);

                lblManagerHome.Text = timhome.Rows[0][1].ToString();
                lblCaptainHome.Text = timhome.Rows[0][2].ToString();
                lblStadium.Text = timhome.Rows[0][5].ToString();
                lblCapacity.Text = timhome.Rows[0][4].ToString();
            }
            catch (Exception ex)
            {
                   
                
            }


        }

        private void cbLawan_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable timlawan = new DataTable();
                sqlQuery = "SELECT t.team_name as `Nama Tim`, m.manager_name as `Nama Man`, p.player_name, t.team_id as `ID TEAM` FROM team t, manager m, player p WHERE m.manager_id = t.manager_id and t.captain_id = p.player_id And t.team_id = '" + cbLawan.SelectedValue.ToString() + "'";
                sqlCommand = new MySqlCommand(sqlQuery, sqlConnect);
                sqlAdapter = new MySqlDataAdapter(sqlCommand);
                sqlAdapter.Fill(timlawan);
                
                lblManagerLawan.Text = timlawan.Rows[0][1].ToString();
                lblCaptainLawan.Text = timlawan.Rows[0][2].ToString();


            }
            catch (Exception ex)
            {
                

            }



        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable Tanding = new DataTable();

                DataTable Tanding2 = new DataTable();
                sqlQuery = $"Select m.match_id , date_format(m.match_date, '%d %M %Y' ), concat(m.goal_home, ' - ', m.goal_away) as 'Skor' FROM `match` m WHERE m.team_home = '{cbHome.SelectedValue}' AND m.team_away = '{cbLawan.SelectedValue}';";
                sqlCommand = new MySqlCommand(sqlQuery, sqlConnect);
                sqlAdapter = new MySqlDataAdapter(sqlCommand);
                sqlAdapter.Fill(Tanding2);

                lblTanggal.Text = Tanding2.Rows[0][1].ToString();
                lblSkor.Text = Tanding2.Rows[0][2].ToString();



                sqlQuery = "Select d.minute as 'minute', p.player_name as 'Player Name 1', if (d.type = 'CY', 'YELLOW CARD', if (d.type = 'CR', 'RED CARD', if (d.type = 'GO', 'GOAL', if (d.type = 'GP', 'GOAL PENALTY', if (d.type = 'GW', 'OWN GOAL', 'PENALTY MISS'))))) as 'Type 1', p2.player_name as 'Player Name 2', if (d2.type = 'CY', 'YELLOW CARD', if (d2.type = 'CR', 'RED CARD', if (d2.type = 'GO', 'GOAL', if (d2.type = 'GP', 'GOAL PENALTY', if (d2.type = 'GW', 'OWN GOAL', 'PENALTY MISS'))))) as 'Type 2' FROM dmatch d, dmatch d2, player p, player p2 WHERE p.player_id = d.player_id and p2.player_id = d2.player_id and d.match_id = " + Tanding2.Rows[0][0].ToString() + " and d2.match_id = " + Tanding2.Rows[0][0].ToString() + " ";
                sqlCommand = new MySqlCommand(sqlQuery, sqlConnect);
                sqlAdapter = new MySqlDataAdapter(sqlCommand);
                sqlAdapter.Fill(Tanding);


                dgvMatch.DataSource = Tanding;

            }
            catch (Exception ex)
            {
                

            }
        }
    }
}
