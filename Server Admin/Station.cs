﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Server_Admin
{
    public class Station
    {
        public bool IsAlive { get; set; }
        public string Server { get; set; }
        public string ServerNumber { get; set; }
        public string IP { get; set; }
        public int SteeringHelp { get; set; }
        public int BrakingHelp { get; set; }
        public int StabilityControl { get; set; }
        public int AutoShifting { get; set; }
        public int ThrottleControl { get; set; }
        public int AntiLockBrakes { get; set; }
        public int DrivingLine { get; set; }
        public int AutoReverse { get; set; }
        public string Name { get; set; }
        public string Nick { get; set; }

        public Station(bool isAlive, string server, string serverNumber, string ip, int steeringHelp, int brakingHelp, int stabilityControl, int autoShifting, int throttleControl, int antiLockBrakes, int drivingLine, int autoReverse, string name, string nick)
        {
            IsAlive = isAlive;
            Server = server;
            ServerNumber = serverNumber;
            IP = ip;
            SteeringHelp = steeringHelp;
            BrakingHelp = brakingHelp;
            StabilityControl = stabilityControl;
            AutoShifting = autoShifting;
            ThrottleControl = throttleControl;
            AntiLockBrakes = antiLockBrakes;
            DrivingLine = drivingLine;
            AutoReverse = autoReverse;
            Name = name;
            Nick = nick;
        }

        public Station(int steeringHelp, int brakingHelp, int stabilityControl, int autoShifting, int throttleControl, int antiLockBrakes, int drivingLine, int autoReverse)
        {
            IsAlive = false;
            Server = "";
            ServerNumber = "";
            IP = "";
            SteeringHelp = steeringHelp;
            BrakingHelp = brakingHelp;
            StabilityControl = stabilityControl;
            AutoShifting = autoShifting;
            ThrottleControl = throttleControl;
            AntiLockBrakes = antiLockBrakes;
            DrivingLine = drivingLine;
            AutoReverse = autoReverse;
            Name = "Jugador";
            Nick = "Jugador";
        }

        public Station()
        {
            IsAlive = false;
            Server = "";
            ServerNumber = "";
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
        }

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
                        MessageBox.Show($"{responseBody}");
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
                        MessageBox.Show($"{responseBody}");
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

        public async Task<bool> SendJoinRequest(bool connectDirectly = false)
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
                        if (connectDirectly)
                        {
                            await Task.Delay(5000);
                            url = $"http://{server}:5397/navigation/action/NAV_RACE_MULTIPLAYER"; // Poner la url de ir a multiplayer
                            response = await client.PostAsync(url, new StringContent(string.Empty));
                            if (response.IsSuccessStatusCode)
                            {
                                return true;
                            }
                            else
                            {
                                MessageBox.Show($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                                return false;
                            }
                        }
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

    }
}
