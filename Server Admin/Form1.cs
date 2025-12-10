using System.Collections.Generic;
using System.IO;
using System.Security.Policy;
using System.Text.Json;

namespace Server_Admin
{
    public partial class Form1 : Form
    {
        private Station easyStation;
        private Station mediumStation;
        private Station hardStation;
        private Station manualStation;
        // Holds station values
        private Station station1;
        private Station station2;
        private Station station3;
        private Station station4;
        private Station station5;
        private Station station6;
        private Station station7;
        private Station station8;
        private Station station9;

        private Dictionary<string, string> machines;
        private Dictionary<string, string> servers;

        public Form1()
        {
            InitializeComponent();
            loadOptions();
            loadServers();
            loadStations();
            loadMachines();
            // El texto vendr� de la api en formato json
            //string jsonString = File.ReadAllText("C:/Users/alexm/source/repos/RFactor2ControlPanel/Server Admin/Assets/player.json");
            //var optionsDictionary = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonString);
            //var drivingAids = JsonSerializer.Deserialize<Dictionary<string, object>>(optionsDictionary?["DRIVING AIDS"]?.ToString() ?? "");
            //jsonString = JsonSerializer.Serialize(optionsDictionary, new JsonSerializerOptions { WriteIndented = true });
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        #region Save station buttons (Saves station data to data file)
        private async void btnSaveStation1_Click(object sender, EventArgs e)
        {
            string selectedServer = cbServer1.SelectedValue?.ToString() ?? null;
            station1.Server = selectedServer;
            station1.Name = txtNameStation1.Text;
            station1.Nick = txtNameStation1.Text;
            saveStations();
            await station1.SendSaveRequest();
        }
        private async void btnSaveStation2_Click(object sender, EventArgs e)
        {
            string selectedServer = cbServer2.SelectedValue?.ToString() ?? null;
            station2.Server = selectedServer;
            station2.Name = txtNameStation2.Text;
            station2.Nick = txtNameStation2.Text;
            saveStations();
            await station2.SendSaveRequest();
        }
        private async void btnSaveStation3_Click(object sender, EventArgs e)
        {
            string selectedServer = cbServer3.SelectedValue?.ToString() ?? null;
            station3.Server = selectedServer;
            station3.Name = txtNameStation3.Text;
            station3.Nick = txtNameStation3.Text;
            saveStations();
            await station3.SendSaveRequest();
        }
        private async void btnSaveStation4_Click(object sender, EventArgs e)
        {
            string selectedServer = cbServer4.SelectedValue?.ToString() ?? null;
            station4.Server = selectedServer;
            station4.Name = txtNameStation4.Text;
            station4.Nick = txtNameStation4.Text;
            saveStations();
            await station4.SendSaveRequest();
        }
        private async void btnSaveStation5_Click(object sender, EventArgs e)
        {
            string selectedServer = cbServer5.SelectedValue?.ToString() ?? null;
            station5.Server = selectedServer;
            station5.Name = txtNameStation5.Text;
            station5.Nick = txtNameStation5.Text;
            saveStations();
            await station5.SendSaveRequest();
        }
        private async void btnSaveStation6_Click(object sender, EventArgs e)
        {
            string selectedServer = cbServer6.SelectedValue?.ToString() ?? null;
            station6.Server = selectedServer;
            station6.Name = txtNameStation6.Text;
            station6.Nick = txtNameStation6.Text;
            saveStations();
            await station6.SendSaveRequest();
        }
        private async void btnSaveStation7_Click(object sender, EventArgs e)
        {
            string selectedServer = cbServer7.SelectedValue?.ToString() ?? null;
            station7.Server = selectedServer;
            station7.Name = txtNameStation7.Text;
            station7.Nick = txtNameStation7.Text;
            saveStations();
            await station7.SendSaveRequest();
        }
        private async void btnSaveStation8_Click(object sender, EventArgs e)
        {
            string selectedServer = cbServer8.SelectedValue?.ToString() ?? null;
            station8.Server = selectedServer;
            station8.Name = txtNameStation8.Text;
            station8.Nick = txtNameStation8.Text;
            saveStations();
            await station8.SendSaveRequest();
        }
        private async void btnSaveStation9_Click(object sender, EventArgs e)
        {
            string selectedServer = cbServer9.SelectedValue?.ToString() ?? null;
            station9.Server = selectedServer;
            station9.Name = txtNameStation9.Text;
            station9.Nick = txtNameStation9.Text;
            saveStations();
            await station9.SendSaveRequest();
        }
        #endregion

        #region Custom options buttons (Unused)
        private void btnOptionsStation1_Click(object sender, EventArgs e)
        {

        }

        private void btnOptionsStation2_Click(object sender, EventArgs e)
        {

        }

        private void btnOptionsStation3_Click(object sender, EventArgs e)
        {

        }

        private void btnOptionsStation4_Click(object sender, EventArgs e)
        {

        }

        private void btnOptionsStation5_Click(object sender, EventArgs e)
        {

        }

        private void btnOptionsStation6_Click(object sender, EventArgs e)
        {

        }

        private void btnOptionsStation7_Click(object sender, EventArgs e)
        {

        }

        private void btnOptionsStation8_Click(object sender, EventArgs e)
        {

        }
        private void btnOptionsStation9_Click(object sender, EventArgs e)
        {

        }
        #endregion

        # region Toggle state buttons (Buttons to open and close the game)
        private async void btnToggleState1_Click(object sender, EventArgs e)
        {
            bool result = await station1.SendToggleRequest();
            if (result)
            {
                if (station1.IsAlive)
                    btnToggleState1.BackColor = Color.Lime;
                else
                    btnToggleState1.BackColor = Color.FromArgb(255, 128, 128);
            }
        }

        private async void btnToggleState2_Click(object sender, EventArgs e)
        {
            bool result = await station2.SendToggleRequest();
            if (result)
            {
                if (station2.IsAlive)
                    btnToggleState2.BackColor = Color.Lime;
                else
                    btnToggleState2.BackColor = Color.FromArgb(255, 128, 128);
            }
        }

        private async void btnToggleState3_Click(object sender, EventArgs e)
        {
            bool result = await station3.SendToggleRequest();
            if (result)
            {
                if (station3.IsAlive)
                    btnToggleState3.BackColor = Color.Lime;
                else
                    btnToggleState3.BackColor = Color.FromArgb(255, 128, 128);
            }
        }

        private async void btnToggleState4_Click(object sender, EventArgs e)
        {
            bool result = await station4.SendToggleRequest();
            if (result)
            {
                if (station4.IsAlive)
                    btnToggleState4.BackColor = Color.Lime;
                else
                    btnToggleState4.BackColor = Color.FromArgb(255, 128, 128);
            }
        }

        private async void btnToggleState5_Click(object sender, EventArgs e)
        {
            bool result = await station5.SendToggleRequest();
            if (result)
            {
                if (station5.IsAlive)
                    btnToggleState5.BackColor = Color.Lime;
                else
                    btnToggleState5.BackColor = Color.FromArgb(255, 128, 128);
            }
        }

        private async void btnToggleState6_Click(object sender, EventArgs e)
        {
            bool result = await station6.SendToggleRequest();
            if (result)
            {
                if (station6.IsAlive)
                    btnToggleState6.BackColor = Color.Lime;
                else
                    btnToggleState6.BackColor = Color.FromArgb(255, 128, 128);
            }
        }

        private async void btnToggleState7_Click(object sender, EventArgs e)
        {
            bool result = await station7.SendToggleRequest();
            if (result)
            {
                if (station7.IsAlive)
                    btnToggleState7.BackColor = Color.Lime;
                else
                    btnToggleState7.BackColor = Color.FromArgb(255, 128, 128);
            }
        }

        private async void btnToggleState8_Click(object sender, EventArgs e)
        {
            bool result = await station8.SendToggleRequest();
            if (result)
            {
                if (station8.IsAlive)
                    btnToggleState8.BackColor = Color.Lime;
                else
                    btnToggleState8.BackColor = Color.FromArgb(255, 128, 128);
            }
        }

        private async void btnToggleState9_Click(object sender, EventArgs e)
        {
            bool result = await station9.SendToggleRequest();
            if (result)
            {
                if (station9.IsAlive)
                    btnToggleState9.BackColor = Color.Lime;
                else
                    btnToggleState9.BackColor = Color.FromArgb(255, 128, 128);
            }
        }
        #endregion

        #region Difficulty buttons (Changes station difficulty)
        private void btnEasyStation1_Click(object sender, EventArgs e)
        {
            station1.CopyStationDifficulty(easyStation);
        }

        private void btnMediumStation1_Click(object sender, EventArgs e)
        {
            station1.CopyStationDifficulty(mediumStation);
        }

        private void btnHardStation1_Click(object sender, EventArgs e)
        {
            station1.CopyStationDifficulty(hardStation);
        }

        private void btnManualStation1_Click(object sender, EventArgs e)
        {
            station1.CopyStationDifficulty(manualStation);
        }

        private void btnEasyStation2_Click(object sender, EventArgs e)
        {
            station2.CopyStationDifficulty(easyStation);
        }

        private void btnMediumStation2_Click(object sender, EventArgs e)
        {
            station2.CopyStationDifficulty(mediumStation);
        }

        private void btnHardStation2_Click(object sender, EventArgs e)
        {
            station2.CopyStationDifficulty(hardStation);
        }

        private void btnManualStation2_Click(object sender, EventArgs e)
        {
            station2.CopyStationDifficulty(manualStation);
        }

        private void btnEasyStation3_Click(object sender, EventArgs e)
        {
            station3.CopyStationDifficulty(easyStation);
        }

        private void btnMediumStation3_Click(object sender, EventArgs e)
        {
            station3.CopyStationDifficulty(mediumStation);
        }

        private void btnHardStation3_Click(object sender, EventArgs e)
        {
            station3.CopyStationDifficulty(hardStation);
        }

        private void btnManualStation3_Click(object sender, EventArgs e)
        {
            station3.CopyStationDifficulty(manualStation);
        }

        private void btnEasyStation4_Click(object sender, EventArgs e)
        {
            station4.CopyStationDifficulty(easyStation);
        }

        private void btnMediumStation4_Click(object sender, EventArgs e)
        {
            station4.CopyStationDifficulty(mediumStation);
        }

        private void btnHardStation4_Click(object sender, EventArgs e)
        {
            station4.CopyStationDifficulty(hardStation);
        }

        private void btnManualStation4_Click(object sender, EventArgs e)
        {
            station4.CopyStationDifficulty(manualStation);
        }

        private void btnEasyStation5_Click(object sender, EventArgs e)
        {
            station5.CopyStationDifficulty(easyStation);
        }

        private void btnMediumStation5_Click(object sender, EventArgs e)
        {
            station5.CopyStationDifficulty(mediumStation);
        }

        private void btnHardStation5_Click(object sender, EventArgs e)
        {
            station5.CopyStationDifficulty(hardStation);
        }

        private void btnManualStation5_Click(object sender, EventArgs e)
        {
            station5.CopyStationDifficulty(manualStation);
        }

        private void btnEasyStation6_Click(object sender, EventArgs e)
        {
            station6.CopyStationDifficulty(easyStation);
        }

        private void btnMediumStation6_Click(object sender, EventArgs e)
        {
            station6.CopyStationDifficulty(mediumStation);
        }

        private void btnHardStation6_Click(object sender, EventArgs e)
        {
            station6.CopyStationDifficulty(hardStation);
        }

        private void btnManualStation6_Click(object sender, EventArgs e)
        {
            station6.CopyStationDifficulty(manualStation);
        }

        private void btnEasyStation7_Click(object sender, EventArgs e)
        {
            station7.CopyStationDifficulty(easyStation);
        }

        private void btnMediumStation7_Click(object sender, EventArgs e)
        {
            station7.CopyStationDifficulty(mediumStation);
        }

        private void btnHardStation7_Click(object sender, EventArgs e)
        {
            station7.CopyStationDifficulty(hardStation);
        }

        private void btnManualStation7_Click(object sender, EventArgs e)
        {
            station7.CopyStationDifficulty(manualStation);
        }

        private void btnEasyStation8_Click(object sender, EventArgs e)
        {
            station8.CopyStationDifficulty(easyStation);
        }

        private void btnMediumStation8_Click(object sender, EventArgs e)
        {
            station8.CopyStationDifficulty(mediumStation);
        }

        private void btnHardStation8_Click(object sender, EventArgs e)
        {
            station8.CopyStationDifficulty(hardStation);
        }

        private void btnManualStation8_Click(object sender, EventArgs e)
        {
            station8.CopyStationDifficulty(manualStation);
        }

        private void btnEasyStation9_Click(object sender, EventArgs e)
        {
            station9.CopyStationDifficulty(easyStation);
        }

        private void btnMediumStation9_Click(object sender, EventArgs e)
        {
            station9.CopyStationDifficulty(mediumStation);
        }

        private void btnHardStation9_Click(object sender, EventArgs e)
        {
            station9.CopyStationDifficulty(hardStation);
        }

        private void btnManualStation9_Click(object sender, EventArgs e)
        {
            station9.CopyStationDifficulty(manualStation);
        }
        #endregion

        #region End buttons (End race and return to information screen)
        private async void btnEndRace1_Click(object sender, EventArgs e)
        {
            saveStationConnections(station1, false);
            await station1.SendFinishRaceRequest();
        }

        private async void btnEndRace2_Click(object sender, EventArgs e)
        {
            saveStationConnections(station2, false);
            await station2.SendFinishRaceRequest();
        }

        private async void btnEndRace3_Click(object sender, EventArgs e)
        {
            saveStationConnections(station3, false);
            await station3.SendFinishRaceRequest();
        }

        private async void btnEndRace4_Click(object sender, EventArgs e)
        {
            saveStationConnections(station4, false);
            await station4.SendFinishRaceRequest();
        }

        private async void btnEndRace5_Click(object sender, EventArgs e)
        {
            saveStationConnections(station5, false);
            await station5.SendFinishRaceRequest();
        }

        private async void btnEndRace6_Click(object sender, EventArgs e)
        {
            saveStationConnections(station6, false);
            await station6.SendFinishRaceRequest();
        }

        private async void btnEndRace7_Click(object sender, EventArgs e)
        {
            saveStationConnections(station7, false);
            await station7.SendFinishRaceRequest();
        }

        private async void btnEndRace8_Click(object sender, EventArgs e)
        {
            saveStationConnections(station8, false);
            await station8.SendFinishRaceRequest();
        }

        private async void btnEndRace9_Click(object sender, EventArgs e)
        {
            saveStationConnections(station9, false);
            await station9.SendFinishRaceRequest();
        }
        #endregion

        #region Game join buttons (Join stattion to a multiplayer game)
        private async void btnConnectStation1_Click(object sender, EventArgs e)
        {
            saveStationConnections(station1, true);
            await station1.SendJoinRequest();
        }

        private async void btnConnectStation2_Click(object sender, EventArgs e)
        {
            saveStationConnections(station2, true);
            await station2.SendJoinRequest();
        }

        private async void btnConnectStation3_Click(object sender, EventArgs e)
        {
            saveStationConnections(station3, true);
            await station3.SendJoinRequest();
        }

        private async void btnConnectStation4_Click(object sender, EventArgs e)
        {
            saveStationConnections(station4, true);
            await station4.SendJoinRequest();
        }

        private async void btnConnectStation5_Click(object sender, EventArgs e)
        {
            saveStationConnections(station5, true);
            await station5.SendJoinRequest();
        }

        private async void btnConnectStation6_Click(object sender, EventArgs e)
        {
            saveStationConnections(station6, true);
            await station6.SendJoinRequest();
        }

        private async void btnConnectStation7_Click(object sender, EventArgs e)
        {
            saveStationConnections(station7, true);
            await station7.SendJoinRequest();
        }

        private async void btnConnectStation8_Click(object sender, EventArgs e)
        {
            saveStationConnections(station8, true);
            await station8.SendJoinRequest();
        }
        private async void btnConnectStation9_Click(object sender, EventArgs e)
        {
            saveStationConnections(station9, true);
            await station9.SendJoinRequest();
        }
        #endregion

        # region Connect buttons (Multiplayer join modal with Drive button)
        private async void btnConnect1_Click(object sender, EventArgs e)
        {
            await station1.SendDriveMultiplayerRequest();
        }

        private async void btnConnect2_Click(object sender, EventArgs e)
        {
            await station2.SendDriveMultiplayerRequest();
        }

        private async void btnConnect3_Click(object sender, EventArgs e)
        {
            await station3.SendDriveMultiplayerRequest();
        }

        private async void btnConnect4_Click(object sender, EventArgs e)
        {
            await station4.SendDriveMultiplayerRequest();
        }

        private async void btnConnect5_Click(object sender, EventArgs e)
        {
            await station5.SendDriveMultiplayerRequest();
        }

        private async void btnConnect6_Click(object sender, EventArgs e)
        {
            await station6.SendDriveMultiplayerRequest();
        }

        private async void btnConnect7_Click(object sender, EventArgs e)
        {
            await station7.SendDriveMultiplayerRequest();
        }

        private async void btnConnect8_Click(object sender, EventArgs e)
        {
            await station8.SendDriveMultiplayerRequest();
        }

        private async void btnConnect9_Click(object sender, EventArgs e)
        {
            await station9.SendDriveMultiplayerRequest();
        }

        private async void btnDrive1_Click(object sender, EventArgs e)
        {
            await station1.SendDriveRequest();
        }

        private async void btnDrive2_Click(object sender, EventArgs e)
        {
            await station2.SendDriveRequest();
        }
        #endregion

        #region Drive buttons (Information screen with orange Drive button at the bottom)
        private async void btnDrive3_Click(object sender, EventArgs e)
        {
            await station3.SendDriveRequest();
        }

        private async void btnDrive4_Click(object sender, EventArgs e)
        {
            await station4.SendDriveRequest();
        }

        private async void btnDrive5_Click(object sender, EventArgs e)
        {
            await station5.SendDriveRequest();
        }

        private async void btnDrive6_Click(object sender, EventArgs e)
        {
            await station6.SendDriveRequest();
        }

        private async void btnDrive7_Click(object sender, EventArgs e)
        {
            await station7.SendDriveRequest();
        }

        private async void btnDrive8_Click(object sender, EventArgs e)
        {
            await station8.SendDriveRequest();
        }

        private async void btnDrive9_Click(object sender, EventArgs e)
        {
            await station9.SendDriveRequest();
        }
        #endregion

        #region Autodrive buttons (Sends request to enable AI help)
        private async void btnAuto1_Click(object sender, EventArgs e)
        {
            await station1.SendAutoDriveRequest();
        }

        private async void btnAuto2_Click(object sender, EventArgs e)
        {
            await station2.SendAutoDriveRequest();
        }

        private async void btnAuto3_Click(object sender, EventArgs e)
        {
            await station3.SendAutoDriveRequest();
        }

        private async void btnAuto4_Click(object sender, EventArgs e)
        {
            await station4.SendAutoDriveRequest();
        }

        private async void btnAuto5_Click(object sender, EventArgs e)
        {
            await station5.SendAutoDriveRequest();
        }

        private async void btnAuto6_Click(object sender, EventArgs e)
        {
            await station6.SendAutoDriveRequest();
        }

        private async void btnAuto7_Click(object sender, EventArgs e)
        {
            await station7.SendAutoDriveRequest();
        }

        private async void btnAuto8_Click(object sender, EventArgs e)
        {
            await station8.SendAutoDriveRequest();
        }

        private async void btnAuto9_Click(object sender, EventArgs e)
        {
            await station9.SendAutoDriveRequest();
        }
        #endregion

        #region Data saving functions
        private void saveStations()
        {
            List<Station> stationList = new List<Station> { station1, station2, station3, station4, station5, station6, station7, station8, station9 };
            string jsonData = JsonSerializer.Serialize(stationList);
            string programFilesX86 = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86);
            string saveFolder = programFilesX86 + "\\rFactorServerAdmin";
            File.WriteAllText(saveFolder + "\\userData.json", jsonData);
        }
        private void saveStationConnections(Station stationToSave, bool start)
        {
            string programFilesX86 = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86);
            string saveFolder = programFilesX86 + "\\rFactorServerAdmin";
            string filePath = saveFolder + "\\connectionRegistry.json";

            List<Dictionary<string, object>> stations = new List<Dictionary<string, object>>();

            if (File.Exists(filePath))
            {
                string existingJson = File.ReadAllText(filePath);
                if (!string.IsNullOrEmpty(existingJson))
                {
                    stations = JsonSerializer.Deserialize<List<Dictionary<string, object>>>(existingJson);
                }
            }

            var stationDict = JsonSerializer.Deserialize<Dictionary<string, object>>(JsonSerializer.Serialize(stationToSave));

            stationDict["DateAdded"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            if (start)
            {
                stationDict["Action"] = "Start";
            }
            else
            {
                stationDict["Action"] = "End";
            }

            stationDict.Remove("IsAlive");
            stationDict.Remove("SteeringHelp");
            stationDict.Remove("BrakingHelp");
            stationDict.Remove("StabilityControl");
            stationDict.Remove("AutoShifting");
            stationDict.Remove("ThrottleControl");
            stationDict.Remove("AntiLockBrakes");
            stationDict.Remove("DrivingLine");
            stationDict.Remove("AutoReverse");
            stationDict.Remove("Nick");

            stations.Add(stationDict);

            string jsonData = JsonSerializer.Serialize(stations);
            File.WriteAllText(filePath, jsonData);
        }
        #endregion

        #region Data loading functions
        private void SetupComboBox(ComboBox serverComboBox, TextBox textBox, Station station)
        {
            serverComboBox.DataSource = new BindingSource(servers, null);
            serverComboBox.DisplayMember = "Key";
            serverComboBox.ValueMember = "Value";
            if (station.Server != null)
            {
                serverComboBox.SelectedValue = station.Server;
            }

            textBox.Text = station.Name == "Jugador" ? "" : station.Name;
        }
        private void loadStations()
        {
            string programFilesX86 = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86);
            string saveFolder = programFilesX86 + "\\rFactorServerAdmin";
            if (File.Exists(saveFolder + "\\userData.json"))
            {
                string data = File.ReadAllText(saveFolder + "\\userData.json");
                List<Station> stationList = JsonSerializer.Deserialize<List<Station>>(data);

                station1 = stationList[0];
                station2 = stationList[1];
                station3 = stationList[2];
                station4 = stationList[3];
                station5 = stationList[4];
                station6 = stationList[5];
                station7 = stationList[6];
                station8 = stationList[7];
                station9 = stationList[8];


                SetupComboBox(cbServer1, txtNameStation1, station1);
                SetupComboBox(cbServer2, txtNameStation2, station2);
                SetupComboBox(cbServer3, txtNameStation3, station3);
                SetupComboBox(cbServer4, txtNameStation4, station4);
                SetupComboBox(cbServer5, txtNameStation5, station5);
                SetupComboBox(cbServer6, txtNameStation6, station6);
                SetupComboBox(cbServer7, txtNameStation7, station7);
                SetupComboBox(cbServer8, txtNameStation8, station8);
                SetupComboBox(cbServer9, txtNameStation9, station9);
            }
            else
            {
                station1 = new Station();
                station2 = new Station();
                station3 = new Station();
                station4 = new Station();
                station5 = new Station();
                station6 = new Station();
                station7 = new Station();
                station8 = new Station();
                station9 = new Station();
            }
        }
        private void loadOptions()
        {
            string programFilesX86 = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86);
            string saveFolder = programFilesX86 + "\\rFactorServerAdmin";
            if (!Directory.Exists(saveFolder))
            {
                Directory.CreateDirectory(saveFolder);
            }
            if (File.Exists(saveFolder + "\\optionsData.json"))
            {
                string data = File.ReadAllText(saveFolder + "\\optionsData.json");
                using (JsonDocument doc = JsonDocument.Parse(data))
                {
                    JsonElement root = doc.RootElement;

                    JsonElement easyElement = root.GetProperty("easy");
                    easyStation = new Station(
                        easyElement.GetProperty("SteeringHelp").GetInt32(),
                        easyElement.GetProperty("BrakingHelp").GetInt32(),
                        easyElement.GetProperty("StabilityControl").GetInt32(),
                        easyElement.GetProperty("AutoShifting").GetInt32(),
                        easyElement.GetProperty("ThrottleControl").GetInt32(),
                        easyElement.GetProperty("AntiLockBrakes").GetInt32(),
                        easyElement.GetProperty("DrivingLine").GetInt32(),
                        easyElement.GetProperty("AutoReverse").GetInt32(),
                        easyElement.GetProperty("OppositeLock").GetInt32()
                    );

                    JsonElement mediumElement = root.GetProperty("medium");
                    mediumStation = new Station(
                        mediumElement.GetProperty("SteeringHelp").GetInt32(),
                        mediumElement.GetProperty("BrakingHelp").GetInt32(),
                        mediumElement.GetProperty("StabilityControl").GetInt32(),
                        mediumElement.GetProperty("AutoShifting").GetInt32(),
                        mediumElement.GetProperty("ThrottleControl").GetInt32(),
                        mediumElement.GetProperty("AntiLockBrakes").GetInt32(),
                        mediumElement.GetProperty("DrivingLine").GetInt32(),
                        mediumElement.GetProperty("AutoReverse").GetInt32(),
                        mediumElement.GetProperty("OppositeLock").GetInt32()
                    );

                    JsonElement hardElement = root.GetProperty("hard");
                    hardStation = new Station(
                        hardElement.GetProperty("SteeringHelp").GetInt32(),
                        hardElement.GetProperty("BrakingHelp").GetInt32(),
                        hardElement.GetProperty("StabilityControl").GetInt32(),
                        hardElement.GetProperty("AutoShifting").GetInt32(),
                        hardElement.GetProperty("ThrottleControl").GetInt32(),
                        hardElement.GetProperty("AntiLockBrakes").GetInt32(),
                        hardElement.GetProperty("DrivingLine").GetInt32(),
                        hardElement.GetProperty("AutoReverse").GetInt32(),
                        hardElement.GetProperty("OppositeLock").GetInt32()
                    );

                    JsonElement manualElement = root.GetProperty("manual");
                    manualStation = new Station(
                        manualElement.GetProperty("SteeringHelp").GetInt32(),
                        manualElement.GetProperty("BrakingHelp").GetInt32(),
                        manualElement.GetProperty("StabilityControl").GetInt32(),
                        manualElement.GetProperty("AutoShifting").GetInt32(),
                        manualElement.GetProperty("ThrottleControl").GetInt32(),
                        manualElement.GetProperty("AntiLockBrakes").GetInt32(),
                        manualElement.GetProperty("DrivingLine").GetInt32(),
                        manualElement.GetProperty("AutoReverse").GetInt32(),
                        manualElement.GetProperty("OppositeLock").GetInt32()
                    );
                }
            }
            else
            {
                using (StreamWriter sw = File.CreateText(saveFolder + "\\optionsData.json"))
                {
                    sw.Write("{\r\n    \"easy\":{\r\n        \"SteeringHelp\": 1,\r\n        \"BrakingHelp\": 1,\r\n        \"StabilityControl\": 2,\r\n        \"AutoShifting\": 3,\r\n        \"ThrottleControl\": 3,\r\n        \"AntiLockBrakes\": 2,\r\n        \"DrivingLine\": 3,\r\n        \"AutoReverse\": 1,\r\n        \"OppositeLock\":1\r\n    },\r\n    \"medium\":{\r\n        \"SteeringHelp\": 0,\r\n        \"BrakingHelp\": 0,\r\n        \"StabilityControl\": 2,\r\n        \"AutoShifting\": 3,\r\n        \"ThrottleControl\": 3,\r\n        \"AntiLockBrakes\": 2,\r\n        \"DrivingLine\": 3,\r\n        \"AutoReverse\": 1,\r\n        \"OppositeLock\":1\r\n    },\r\n    \"hard\":{\r\n        \"SteeringHelp\": 0,\r\n        \"BrakingHelp\": 0,\r\n        \"StabilityControl\": 1,\r\n        \"AutoShifting\": 0,\r\n        \"ThrottleControl\": 1,\r\n        \"AntiLockBrakes\": 1,\r\n        \"DrivingLine\": 3,\r\n        \"AutoReverse\": 0,\r\n        \"OppositeLock\":1\r\n    },\r\n    \"manual\":{\r\n        \"SteeringHelp\": 0,\r\n        \"BrakingHelp\": 0,\r\n        \"StabilityControl\": 0,\r\n        \"AutoShifting\": 0,\r\n        \"ThrottleControl\": 0,\r\n        \"AntiLockBrakes\": 0,\r\n        \"DrivingLine\": 0,\r\n        \"AutoReverse\": 0,\r\n        \"OppositeLock\":0\r\n    }\r\n}");
                }
            }
        }
        private void loadServers()
        {
            string programFilesX86 = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86);
            string saveFolder = programFilesX86 + "\\rFactorServerAdmin";
            if (!Directory.Exists(saveFolder))
            {
                Directory.CreateDirectory(saveFolder);
            }
            if (File.Exists(saveFolder + "\\serverData.json"))
            {
                string data = File.ReadAllText(saveFolder + "\\serverData.json");
                servers = JsonSerializer.Deserialize<Dictionary<string, string>>(data);
                cbServer1.Items.AddRange(servers.Keys.ToArray());
            }
            else
            {
                using (StreamWriter sw = File.CreateText(saveFolder + "\\serverData.json"))
                {
                    sw.Write("{\r\n    \"Server1\": \"192.84.12.1:5000\",\r\n    \"Server2\": \"192.84.12.2:5000\",\r\n    \"Server3\": \"192.84.12.3:5000\",\r\n    \"Server4\": \"192.84.12.4:5000\"\r\n,\r\n    \"Server5\": \"192.84.12.4:5000\"\r\n,\r\n    \"Server6\": \"192.84.12.4:5000\"\r\n}");
                }
            }
        }
        private void loadMachines()
        {
            string programFilesX86 = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86);
            string saveFolder = programFilesX86 + "\\rFactorServerAdmin";
            if (!Directory.Exists(saveFolder))
            {
                Directory.CreateDirectory(saveFolder);
            }
            if (File.Exists(saveFolder + "\\machineData.json"))
            {
                string data = File.ReadAllText(saveFolder + "\\machineData.json");
                machines = JsonSerializer.Deserialize<Dictionary<string, string>>(data);
                for (int i = 0; i < 8; i++)
                {
                    string key = "Station " + (i + 1);
                    if (machines.TryGetValue(key, out string ip))
                    {
                        switch (i)
                        {
                            case 0:
                                station1.IP = ip;
                                break;
                            case 1:
                                station2.IP = ip;
                                break;
                            case 2:
                                station3.IP = ip;
                                break;
                            case 3:
                                station4.IP = ip;
                                break;
                            case 4:
                                station5.IP = ip;
                                break;
                            case 5:
                                station6.IP = ip;
                                break;
                            case 6:
                                station7.IP = ip;
                                break;
                            case 7:
                                station8.IP = ip;
                                break;
                            case 8:
                                station9.IP = ip;
                                break;
                        }
                    }
                }
            }
            else
            {
                using (StreamWriter sw = File.CreateText(saveFolder + "\\machineData.json"))
                {
                    sw.Write("{\r\n    \"Station 1\": \"127.0.0.1:5000\",\r\n    \"Station 2\": \"192.84.12.2:5000\",\r\n    \"Station 3\": \"192.84.12.3:5000\",\r\n    \"Station 4\": \"192.84.12.3:5000\",\r\n    \"Station 5\": \"192.84.12.3:5000\",\r\n    \"Station 6\": \"192.84.12.3:5000\",\r\n    \"Station 7\": \"192.84.12.3:5000\",\r\n    \"Station 8\": \"192.84.12.4:5000\"\r\n,\r\n    \"Station 9\": \"192.84.12.5:5000\"\r\n}");
                }
            }
        }
        #endregion

        #region Global buttons

        private async void btnConnect1Global_Click(object sender, EventArgs e)
        {
            if(servers.TryGetValue("Server1", out string address))
            {
                setAllStations(address);
            }
            else
            {
                MessageBox.Show("Configura la lista de servidores con los nombres sugeridos (Server[num])");
            }
            await connectAllStations();
        }

        private async void btnConnect2Global_Click(object sender, EventArgs e)
        {
            if (servers.TryGetValue("Server2", out string address))
            {
                setAllStations(address);
            }
            else
            {
                MessageBox.Show("Configura la lista de servidores con los nombres sugeridos (Server[num])");
            }
            await connectAllStations();
        }

        private async void btnConnect3Global_Click(object sender, EventArgs e)
        {
            if (servers.TryGetValue("Server3", out string address))
            {
                setAllStations(address);
            }
            else
            {
                MessageBox.Show("Configura la lista de servidores con los nombres sugeridos (Server[num])");
            }
            await connectAllStations();
        }

        private async void btnConnect4Global_Click(object sender, EventArgs e)
        {
            if (servers.TryGetValue("Server4", out string address))
            {
                setAllStations(address);
            }
            else
            {
                MessageBox.Show("Configura la lista de servidores con los nombres sugeridos (Server[num])");
            }
            await connectAllStations();
        }

        private async void btnConnect5Global_Click(object sender, EventArgs e)
        {
            if (servers.TryGetValue("Server5", out string address))
            {
                setAllStations(address);
            }
            else
            {
                MessageBox.Show("Configura la lista de servidores con los nombres sugeridos (Server[num])");
            }
            await connectAllStations();
        }

        private async void btnConnect6Global_Click(object sender, EventArgs e)
        {
            if (servers.TryGetValue("Server6", out string address))
            {
                setAllStations(address);
            }
            else
            {
                MessageBox.Show("Configura la lista de servidores con los nombres sugeridos (Server[num])");
            }
            await connectAllStations();
        }

        private async void btnEndRace1Global_Click(object sender, EventArgs e)
        {
            await endAllStations();
        }

        private async void btnEndRace2Global_Click(object sender, EventArgs e)
        {
            await endAllStations();
        }

        private async void btnEndRace3Global_Click(object sender, EventArgs e)
        {
            await endAllStations();
        }

        private async void btnEndRace4Global_Click(object sender, EventArgs e)
        {
            await endAllStations();
        }

        private async void btnEndRace5Global_Click(object sender, EventArgs e)
        {
            await endAllStations();
        }

        private async void btnEndRace6Global_Click(object sender, EventArgs e)
        {
            await endAllStations();
        }

        private void setAllStations(String server)
        {
            station1.Server = server;
            station2.Server = server;
            station3.Server = server;
            station4.Server = server;
            station5.Server = server;
            station6.Server = server;
            station7.Server = server;
            station8.Server = server;
            station9.Server = server;
        }

        // @TODO: Individual stations should be converted into an array or even a dictionary for easier reading and iteration
        private async Task<bool> connectAllStations()
        {
            if (station1.IsAlive)
                await station1.SendJoinRequest();
            if (station2.IsAlive)
                await station2.SendJoinRequest();
            if (station3.IsAlive)
                await station3.SendJoinRequest();
            if (station4.IsAlive)
                await station4.SendJoinRequest();
            if (station5.IsAlive)
                await station5.SendJoinRequest();
            if (station6.IsAlive)
                await station6.SendJoinRequest();
            if (station7.IsAlive)
                await station7.SendJoinRequest();
            if (station8.IsAlive)
                await station8.SendJoinRequest();
            if (station9.IsAlive)
                await station9.SendJoinRequest();
            return true;
        }

        private async Task<bool> endAllStations()
        {
            if (station1.IsAlive)
                await station1.SendFinishRaceRequest();
            if (station2.IsAlive)
                await station2.SendFinishRaceRequest();
            if (station3.IsAlive)
                await station3.SendFinishRaceRequest();
            if (station4.IsAlive)
                await station4.SendFinishRaceRequest();
            if (station5.IsAlive)
                await station5.SendFinishRaceRequest();
            if (station6.IsAlive)
                await station6.SendFinishRaceRequest();
            if (station7.IsAlive)
                await station7.SendFinishRaceRequest();
            if (station8.IsAlive)
                await station8.SendFinishRaceRequest();
            if (station9.IsAlive)
                await station9.SendFinishRaceRequest();
            return true;
        }
        #endregion
    }
}
