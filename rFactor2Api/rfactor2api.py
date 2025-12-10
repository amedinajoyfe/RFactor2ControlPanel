from flask import Flask, request, jsonify
import os, subprocess, json, requests, pydirectinput, time
app = Flask(__name__)

@app.route('/get_file', methods=['GET'])
def get_file():
    try:
        # Change file path
        file_path = "C:/Program Files (x86)/Steam/steamapps/common/rFactor 2/UserData/player/player.json"
        controller_file_path = "C:/Program Files (x86)/Steam/steamapps/common/rFactor 2/UserData/player/Controller.json"
        with open(file_path, 'r') as f:
            # Get data from the options file and send as response
            data = json.load(f)
            valuesDictionary:dict = {'Steering Help': 0, 'Brake Help': 0, 'Stability Control':0, 'Shift Mode':0, 'Throttle Control':0, 'Antilock Brakes': 0, 'Driving Line': 0, 'Auto Reverse': 0, 'Opposite Lock': 0, 'Player Name': "", 'Player Nick': ""}

            drivingAids = data['DRIVING AIDS']
            driver = data['DRIVER']

            valuesDictionary['Steering Help'] = drivingAids['Steering Help']
            valuesDictionary['Brake Help'] = drivingAids['Brake Help']
            valuesDictionary['Stability Control'] = drivingAids['Stability Control']
            valuesDictionary['Shift Mode'] = drivingAids['Shift Mode']
            valuesDictionary['Throttle Control'] = drivingAids['Throttle Control']
            valuesDictionary['Antilock Brakes'] = drivingAids['Antilock Brakes']
            valuesDictionary['Opposite Lock'] = drivingAids['Opposite Lock']
            valuesDictionary['Driving Line'] = drivingAids['Driving Line']
            valuesDictionary['Player Name'] = driver['Player Name']
            valuesDictionary['Player Nick'] = driver['Player Nick']
            valuesDictionary['Repeat Shifts'] = drivingAids['Repeat Shifts']
            valuesDictionary['Invulnerability'] = drivingAids['Invulnerability']
            
            # Only return necessary options
            with open(controller_file_path, 'r') as f:
                data = json.load(f)
                controls = data['General Controls']
                valuesDictionary['Auto Reverse'] = controls['Auto Reverse']
        return jsonify(valuesDictionary), 200
    except Exception as e:
        return jsonify({"result": "error", "error": str(e)}), 500
    

@app.route('/modify_file', methods=['POST'])
def modify_file():
    try:
        # Change file path
        file_path = "C:/Program Files (x86)/Steam/steamapps/common/rFactor 2/UserData/player/player.json"
        controller_file_path = "C:/Program Files (x86)/Steam/steamapps/common/rFactor 2/UserData/player/Controller.json"
        data = request.get_json(force=True)
        with open(file_path, 'r') as f:
            # Modify file to save options
            fileData = json.load(f)

            fileData['DRIVING AIDS']['Steering Help'] = data['Steering Help']
            fileData['DRIVING AIDS']['Brake Help'] = data['Brake Help']
            fileData['DRIVING AIDS']['Stability Control'] = data['Stability Control']
            fileData['DRIVING AIDS']['Shift Mode'] = data['Shift Mode']
            fileData['DRIVING AIDS']['Throttle Control'] = data['Throttle Control']
            fileData['DRIVING AIDS']['Antilock Brakes'] = data['Antilock Brakes']
            fileData['DRIVING AIDS']['Opposite Lock'] = data['Opposite Lock']
            fileData['DRIVING AIDS']['Driving Line'] = data['Driving Line']
            fileData['DRIVING AIDS']['Repeat Shifts'] = 5
            fileData['DRIVING AIDS']['Invulnerability'] = 1
            fileData['DRIVER']['Player Name'] = data['Player Name']
            fileData['DRIVER']['Player Nick'] = data['Player Name']

        with open(file_path, 'w') as f:
            json.dump(fileData, f, indent = 4)

        with open(controller_file_path, 'r') as f:
            fileData = json.load(f)
            fileData['General Controls']['Auto Reverse'] = data['Auto Reverse']
        with open(controller_file_path, 'w') as f:
            json.dump(fileData, f, indent = 4)

        return jsonify({"result": "success", "message": "File modified successfully."}), 200
    except Exception as e:
        return jsonify({"result": "error", "error": str(e)}), 500

@app.route('/open_game', methods=['POST'])
def open_game():
    try:
        # Change file path
        game_path = "C:/Program Files (x86)/Steam/steamapps/common/rFactor 2/Bin64/rFactor2.exe"
        process = subprocess.Popen([game_path])

        import time
        time.sleep(5)
        
        if process.poll() is None:
            return jsonify({"result": "error", "message": "Error opening game."}), 500
        else:
            return jsonify({"result": "success", "message": "Game opened successfully."}), 200
    except Exception as e:
        return jsonify({"error": str(e)}), 500

@app.route('/close_game', methods=['POST'])
def close_game():
    try:
        # Check name in task manager
        import time
        url = "http://localhost:5397/navigation/action/NAV_TO_MAIN_MENU"

        requests.post(url)
        time.sleep(5)
        game_name = "rFactor2"
        result = os.system(f"taskkill /IM {game_name}.exe /F")
        if result == 0:
            return jsonify({"result": "success", "message": f"{game_name} closed successfully."}), 200
        else:
            return jsonify({"result": "error", "message": f"Error closing {game_name}."}), 500
    except Exception as e:
        return jsonify({"error": str(e)}), 500

@app.route('/autodrive', methods=['POST'])
def autodrive():
    try:
        pydirectinput.press('i')
        return jsonify({"result": "success", "message": "Autodrive toggled succesfully"}), 200
    except Exception as e:
        return jsonify({"error": str(e)}), 500

@app.route('/click', methods=['POST'])
def click_endpoint():
    try:
        data = request.get_json()
        x = data.get('x')
        y = data.get('y')

        if x is None or y is None:
            return jsonify({"error": "Missing x or y parameter"}), 400

        pydirectinput.moveTo(x, y)
        pydirectinput.click()

        return jsonify({"result": "success", "message": "Car selection screen reached successfully"}), 200

    except Exception as e:
        return jsonify({"error": str(e)}), 500

if __name__ == '__main__':
    app.run(host='0.0.0.0', port=5000)
