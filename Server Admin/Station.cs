using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Server_Admin
{
    public class Station
    {
        [JsonIgnore]
        public bool IsAlive { get; set; }
        public string Server { get; set; }
        public string IP { get; set; }
        public int SteeringHelp { get; set; }
        public int BrakingHelp { get; set; }
        public int StabilityControl { get; set; }
        public int AutoShifting { get; set; }
        public int ThrottleControl { get; set; }
        public int AntiLockBrakes { get; set; }
        public int DrivingLine { get; set; }
        public int AutoReverse { get; set; }
        public int OppositeLock { get; set; }
        public string Name { get; set; }
        public string Nick { get; set; }

        public Station(bool isAlive, string server, string ip, int steeringHelp, int brakingHelp, int stabilityControl, int autoShifting, int throttleControl, int antiLockBrakes, int drivingLine, int autoReverse, int oppositeLock, string name, string nick)
        {
            IsAlive = isAlive;
            Server = server;
            IP = ip;
            SteeringHelp = steeringHelp;
            BrakingHelp = brakingHelp;
            StabilityControl = stabilityControl;
            AutoShifting = autoShifting;
            ThrottleControl = throttleControl;
            AntiLockBrakes = antiLockBrakes;
            DrivingLine = drivingLine;
            AutoReverse = autoReverse;
            OppositeLock = oppositeLock;
            Name = name;
            Nick = nick;
        }

        public Station(int steeringHelp, int brakingHelp, int stabilityControl, int autoShifting, int throttleControl, int antiLockBrakes, int drivingLine, int autoReverse, int oppositeLock)
        {
            IsAlive = false;
            Server = "";
            IP = "";
            SteeringHelp = steeringHelp;
            BrakingHelp = brakingHelp;
            StabilityControl = stabilityControl;
            AutoShifting = autoShifting;
            ThrottleControl = throttleControl;
            AntiLockBrakes = antiLockBrakes;
            DrivingLine = drivingLine;
            AutoReverse = autoReverse;
            OppositeLock = oppositeLock;
            Name = "Jugador";
            Nick = "Jugador";
        }

        public Station()
        {
            IsAlive = false;
            Server = "";
            IP = "";
            SteeringHelp = 0;
            BrakingHelp = 0;
            StabilityControl = 0;
            AutoShifting = 0;
            ThrottleControl = 0;
            AntiLockBrakes = 0;
            DrivingLine = 0;
            AutoReverse = 0;
            Name = "Jugador";
            Nick = "Jugador";
        }

        // Set sation difficulty, used by passing a previously configured station 
        public void CopyStationDifficulty(Station stationToCopy)
        {
            SteeringHelp = stationToCopy.SteeringHelp;
            BrakingHelp = stationToCopy.BrakingHelp;
            StabilityControl = stationToCopy.StabilityControl;
            AutoShifting = stationToCopy.AutoShifting;
            ThrottleControl = stationToCopy.ThrottleControl;
            AntiLockBrakes = stationToCopy.AntiLockBrakes;
            DrivingLine = stationToCopy.DrivingLine;
            AutoReverse = stationToCopy.AutoReverse;
            OppositeLock = stationToCopy.OppositeLock;
        }

        #region Request functions
        // Open and close game
        public async Task<bool> SendToggleRequest()
        {
            try
            {
                string[] serverData = IP.Split(':');
                string server = serverData[0];
                string port = serverData[1];
                string url = $"http://{server}:{port}/";

                // Para post
                using (var client = new HttpClient())
                {
                    if (IsAlive)
                    {
                        url += "close_game";
                    }
                    else
                    {
                        url += "open_game";
                    }
                    HttpResponseMessage response = await client.PostAsync(url, null);

                    if (response.IsSuccessStatusCode)
                    {
                        IsAlive = !IsAlive;
                        string responseBody = await response.Content.ReadAsStringAsync();
                        return true;
                    }
                    else
                    {
                        MessageBox.Show($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return false;
            }
        }

        // Set station settings
        public async Task<bool> SendSaveRequest()
        {
            if (IsAlive)
            {
                MessageBox.Show("Apague la máquina antes");
                return false;
            }
            else if (Name == "Jugador" && Nick == "Jugador")
            {
                MessageBox.Show("Introduzca datos para la máquina");
                return false;
            }
            try
            {
                string[] serverData = IP.Split(':');
                string server = serverData[0];
                string port = serverData[1];
                string url = $"http://{server}:{port}/modify_file";

                Dictionary<string, object> options = new Dictionary<string, object>(new[] {
                    new KeyValuePair<string, object>("Steering Help", SteeringHelp),
                    new KeyValuePair<string, object>("Brake Help", BrakingHelp),
                    new KeyValuePair<string, object>("Stability Control", StabilityControl),
                    new KeyValuePair<string, object>("Shift Mode", AutoShifting),
                    new KeyValuePair<string, object>("Throttle Control", ThrottleControl),
                    new KeyValuePair<string, object>("Antilock Brakes", AntiLockBrakes),
                    new KeyValuePair<string, object>("Driving Line", DrivingLine),
                    new KeyValuePair<string, object>("Auto Reverse", AutoReverse),
                    new KeyValuePair<string, object>("Opposite Lock", OppositeLock),
                    new KeyValuePair<string, object>("Player Name", Name),
                    new KeyValuePair<string, object>("Player Nick", Nick)
                });
                string json = JsonSerializer.Serialize(options);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                using (var client = new HttpClient())
                {
                    HttpResponseMessage response = await client.PostAsync(url, content);

                    // Ver como te conectas con la api
                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();
                        return true;
                    }
                    else
                    {
                        MessageBox.Show($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return false;
            }
        }

        // Join server
        public async Task<bool> SendJoinRequest()
        {
            if (!IsAlive)
            {
                MessageBox.Show("Encienda la máquina antes");
                return false;
            }
            else if (Name == "Jugador" && Nick == "Jugador")
            {
                MessageBox.Show("Introduzca datos para la máquina");
                return false;
            }
            try
            {
                string[] serverData = IP.Split(':');
                string server = serverData[0];
                string[] multiServerData = Server.Split(':');
                string multiServer = multiServerData[0];
                string multiPort = multiServerData[1];
                string url = $"http://{server}:5397/rest/multiplayer/join?host={multiServer}&port={multiPort}";


                using (var client = new HttpClient())
                {
                    // Ver como te conectas con la api
                    HttpResponseMessage response = await client.GetAsync(url);
                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();
                        return true;
                    }
                    else
                    {
                        MessageBox.Show($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return false;
            }
        }

        // Join server global
        public async Task<bool> SendJoinRequestGlobal()
        {
            try
            {
                string[] serverData = IP.Split(':');
                string server = serverData[0];
                string[] multiServerData = Server.Split(':');
                string multiServer = multiServerData[0];
                string multiPort = multiServerData[1];
                string url = $"http://{server}:5397/rest/multiplayer/join?host={multiServer}&port={multiPort}";


                using (var client = new HttpClient())
                {
                    // Ver como te conectas con la api
                    HttpResponseMessage response = await client.GetAsync(url);
                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();
                        return true;
                    }
                    else
                    {
                        MessageBox.Show($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return false;
            }
        }

        // Activate AI driving
        public async Task<bool> SendAutoDriveRequest()
        {
            if (!IsAlive)
            {
                MessageBox.Show("Encienda la máquina antes");
                return false;
            }
            else if (Name == "Jugador" && Nick == "Jugador")
            {
                MessageBox.Show("Introduzca datos para la máquina");
                return false;
            }
            try
            {
                string[] serverData = IP.Split(':');
                string server = serverData[0];
                string port = serverData[1];
                string url = $"http://{server}:{port}/autodrive";


                using (var client = new HttpClient())
                {
                    // Ver como te conectas con la api
                    HttpResponseMessage response = await client.PostAsync(url, null);
                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();
                        return true;
                    }
                    else
                    {
                        MessageBox.Show($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                string[] serverData = IP.Split(':');
                string server = serverData[0];
                string port = serverData[1];
                string url = $"http://{server}:{port}/autodrive";
                Console.WriteLine($"Exception: {ex.Message} in url " + url);
                return false;
            }
        }

        // Enter race
        public async Task<bool> SendDriveRequest()
        {
            if (!IsAlive)
            {
                MessageBox.Show("Encienda la máquina antes");
                return false;
            }
            else if (Name == "Jugador" && Nick == "Jugador")
            {
                MessageBox.Show("Introduzca datos para la máquina");
                return false;
            }
            try
            {
                string[] serverData = IP.Split(':');
                string server = serverData[0];
                string[] multiServerData = Server.Split(':');
                string url = $"http://{server}:5397/navigation/action/NAV_TO_REALTIME";


                using (var client = new HttpClient())
                {
                    // Ver como te conectas con la api
                    HttpResponseMessage response = await client.PostAsync(url, null);
                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();
                        return true;
                    }
                    else
                    {
                        MessageBox.Show($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return false;
            }
        }

        // Enter vehicle selection
        public async Task<bool> SendDriveMultiplayerRequest()
        {
            if (!IsAlive)
            {
                MessageBox.Show("Encienda la máquina antes");
                return false;
            }
            else if (Name == "Jugador" && Nick == "Jugador")
            {
                MessageBox.Show("Introduzca datos para la máquina");
                return false;
            }

            try
            {
                string[] serverData = IP.Split(':');
                string server = serverData[0];
                string port = serverData[1];
                string url = $"http://{server}:{port}/click";

                // JSON body with coordinates
                Dictionary<string, object> body = new Dictionary<string, object>()
                {
                    { "x", 3080 },
                    { "y", 540 }
                };
                string json = JsonSerializer.Serialize(body);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                using (var client = new HttpClient())
                {
                    HttpResponseMessage response = await client.PostAsync(url, content);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();
                        Console.WriteLine($"Click response: {responseBody}");
                        return true;
                    }
                    else
                    {
                        MessageBox.Show($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return false;
            }
        }

        // Get station data
        public async Task SendGetRequest()
        {
            try
            {
                string[] serverData = IP.Split(':');
                string server = serverData[0];
                string port = serverData[1];
                string url = $"http://{server}:{port}/get_file";

                using (var client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync(url);
                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();
                        var options = new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        };
                        Dictionary<string, object> dictionary = JsonSerializer.Deserialize<Dictionary<string, object>>(responseBody, options);
                    }
                    else
                    {
                        MessageBox.Show($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
            }
        }

        // Finish race and go back to server info screen
        public async Task SendFinishRaceRequest()
        {
            try
            {
                string[] serverData = IP.Split(':');
                string server = serverData[0];
                string port = serverData[1];
                string url = $"http://{server}:5397/navigation/action/NAV_BACK_TO_EVENT";

                using (var client = new HttpClient())
                {
                    HttpResponseMessage response = await client.PostAsync(url, null);
                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();
                    }
                    else
                    {
                        MessageBox.Show($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
            }
        }
        #endregion
    }
}
