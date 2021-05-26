import json
from flask import Flask, request, jsonify, abort
from decimal import Decimal
from sqlalchemy import create_engine


tasks = [
    {
        'id': 1,
        'title': u'Buy groceries',
        'description': u'Milk, Cheese, Pizza, Fruit, Tylenol', 
        'done': False
    },
    {
        'id': 2,
        'title': u'Learn Python',
        'description': u'Need to find a good Python tutorial on the web', 
        'done': False
    }
]



# Assuming KarDB.db is in your app root folder
e = create_engine('sqlite:///KarDB.db')  # loads db into memory

app = Flask(__name__)

@app.route('/', methods=['GET'])
def get():
    return "Welcome to KARGAME"

@app.route('/petrolbunks', methods=['GET'])
def petrolbunksData():
    conn = e.connect()  # open connection to memory data
    query = conn.execute("select distinct name from PetrolBunks")  # query
    return jsonify({'name': [i[0] for i in query.cursor.fetchall()]})  # format results in dict format


@app.route('/bunks/<int:bunk_id>', methods=['GET'])
def bunkData(bunk_id):  # param is pulled from url string
    conn = e.connect()
    query = conn.execute("select * from PetrolBunks where id='%d'"%bunk_id)
    result = {'data': [dict(zip(tuple (query.keys()) ,i)) for i in query.cursor]}
    return jsonify(result)


@app.route('/fuelCheck/<int:bunk_id>/<float:number>', methods=['GET'])
def fuelDeductCheck(bunk_id, number):  # param must match uri identifier
    conn = e.connect()
    query = conn.execute("select RemainingFuel from PetrolBunks where id='%d'"%bunk_id)
    result = {'data': [dict(zip(tuple (query.keys()) ,i)) for i in query.cursor]}

    '''Check is fuel remaining is positive value'''
    dbRemFuel = result.get('data')[0].get('RemainingFuel')


    a = Decimal(dbRemFuel) - Decimal(number)
    jsonconvert = None
    if a>=0:
        print("Positive fuel" + str(number))
        jsonconvert = {'data': [{"RemainingFuel": str(Decimal(a))}]}
        conn.execute("Update PetrolBunks Set RemainingFuel='%f' Where id='%d'"%(Decimal(a),bunk_id))
    else:
        print("Negative fuel" + str(number))
        jsonconvert = {'data': [{"RemainingFuel": None}]}


    return jsonify(jsonconvert)


@app.route('/playerStats/<string:player_id>', methods=['GET'])
def playerStatsData(player_id):
    conn = e.connect()
    query = conn.execute("select * from PlayerData where ID=?;",(player_id,))
    result = {'data': [dict(zip(tuple (query.keys()) ,i)) for i in query.cursor]}
    
    return jsonify(result)


@app.route('/playerStats/default/<string:player_uuid>', methods=['GET'])
def playerStatsDefaultData(player_uuid):
    #Create new record with new playerID
    conn = e.connect()
    conn.execute("INSERT INTO PlayerData (ID,Timestamp,License2W,License4W,Money,Health,MoneyPerHealth,TotalDistanceTraveled,HN_Dio_Unlocked,HN_Dio_Torque,HN_Dio_MaxSpeed,HN_Dio_TankCapacity,HN_Dio_Mileage,HN_Dio_FR,HN_Dio_TotalDistance,BJ_Chetak_Unlocked,BJ_Chetak_Torque,BJ_Chetak_MaxSpeed,BJ_Chetak_TankCapacity,BJ_Chetak_Mileage,BJ_Chetak_FR,BJ_Chetak_TotalDistance) VALUES ('"+player_uuid+"','22-05-2021 21:41:04','0','0','1000','50','50','0','1','80','85','12','180','1','0','0','50','85','15','120','1','0');")
    query = conn.execute("select * from PlayerData where ID=?;",(player_uuid,))
    result = {'data': [dict(zip(tuple (query.keys()) ,i)) for i in query.cursor]}
    print("Successfully updated default values in db")
        
    return jsonify(result)

# @app.route('/', methods=['PUT'])
# def create_record():
#     record = json.loads(request.data)
#     with open('/tmp/data.txt', 'r') as f:
#         data = f.read()
#     if not data:
#         records = [record]
#     else:
#         records = json.loads(data)
#         records.append(record)
#     with open('/tmp/data.txt', 'w') as f:
#         f.write(json.dumps(records, indent=2))
#     return jsonify(record)

@app.route('/playerprefspush', methods=['POST'])
def update_db_with_pp():
    if not request.json or not 'Timestamp' in request.json:
        print(request.json)
        abort(400)
    print("Successfully posted")

    conn = e.connect()
    conn.execute("Update PlayerData Set Timestamp='"+str(request.json['Timestamp'])+"', License2W='"+str(request.json['License2W'])+"', License4W='"+str(request.json['License4W'])+"', Money='"+str(request.json['Money'])+"', Health='"+str(request.json['Health'])+"', TotalDistanceTraveled='"+str(request.json['TotalDistanceTraveled'])+"', HN_Dio_TotalDistance='"+str(request.json['HN_Dio_TotalDistance'])+"', BJ_Chetak_TotalDistance='"+str(request.json['BJ_Chetak_TotalDistance'])+"' Where ID='"+str(request.json['ID'])+"'")
    # conn.execute("Update PlayerData Set License2W='"+str(request.json['License2W'])+"' Where ID='"+str(request.json['ID'])+"'")

    return "Successfully posted"


@app.route('/todo/api/v1.0/tasks', methods=['POST'])
def create_task():
    if not request.json or not 'title' in request.json:
        abort(400)
    task = {
        'id': tasks[-1]['id'] + 1,
        'title': "Test"+request.json['title'],
        'description': request.json.get('description', ""),
        'done': False
    }
    tasks.append(task)
    return jsonify({'task': task}), 201
    
# @app.route('/', methods=['DELETE'])
# def delte_record():
#     record = json.loads(request.data)
#     new_records = []
#     with open('/tmp/data.txt', 'r') as f:
#         data = f.read()
#         records = json.loads(data)
#         for r in records:
#             if r['name'] == record['name']:
#                 continue
#             new_records.append(r)
#     with open('/tmp/data.txt', 'w') as f:
#         f.write(json.dumps(new_records, indent=2))
#     return jsonify(record)

# app.run(debug=True)

if __name__ == "__main__":
    from waitress import serve
    serve(app, host="0.0.0.0", port=8000)