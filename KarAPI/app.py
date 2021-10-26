import json
from flask import Flask, request, jsonify, abort, make_response
from decimal import Decimal
from sqlalchemy import create_engine
import csv
import jwt 
import datetime
from functools import wraps



# Assuming KarDB.db is in your app root folder
e = create_engine('sqlite:///KarDB.db')  # loads db into memory

app = Flask(__name__)

app.config['SECRET_KEY'] = 'thisisthesecretkey21111993'

@app.route('/', methods=['GET'])
def get():
    return "Welcome to KARGAME"

@app.route('/petrolbunks', methods=['GET'])
def petrolbunksData():
    conn = e.connect()  # open connection to memory data
    query = conn.execute("select distinct name from PetrolBunks")  # query
    return jsonify({'name': [i[0] for i in query.cursor.fetchall()]})  # format results in dict format

#=================FOR XML FORMAT==================
# @app.route('/petrolbunks', methods=['GET'])
# def petrolbunksData():
#     conn = e.connect()  # open connection to memory data
#     query = conn.execute("select distinct name from PetrolBunks")
#     dict = [i[0] for i in query.cursor.fetchall()]
#     xml = dicttoxml(dict, custom_root='name', attr_type=False)
#     response = make_response(xml)                                           
#     response.headers['Content-Type'] = 'application/xml; charset=utf-8'
#     return response
#=================================================


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

    with open('defaultPlayerData.csv', mode='r') as infile:
        reader = csv.reader(infile)
        mydict = {rows[0]:rows[1] for rows in reader}

    print(mydict)
    print()
    # print("INSERT INTO PlayerData (")
    keys = ""
    values = ""
    for key in mydict:
        keys = keys+"'"+key+"'"+","
        values = values+"'"+mydict[key]+"'"+","
    key_str = keys[:-1]
    val_str=values[:-1]
    quer = "INSERT INTO PlayerData (ID,"+key_str+") VALUES ('"+player_uuid+"',"+val_str+");"

    # print(quer)

    #Create new record with new playerID
    conn = e.connect()
    # conn.execute("INSERT INTO PlayerData (ID,Timestamp,2WheelerLicense,4WheelerLicense,MoneyBank,MoneyPocket,Health,MoneyPerHealth,TotalDistanceTraveled,HN-Dio_Unlocked,HN-Dio_Torque,HN-Dio_MaxSpeed,HN-Dio_TankCapacity,HN-Dio_Mileage,HN-Dio_FR,HN-Dio-TankCapacity,BJ-Chetak_Unlocked,BJ-Chetak_Torque,BJ-Chetak_MaxSpeed,BJ-Chetak_TankCapacity,BJ-Chetak_Mileage,BJ-Chetak_FR,BJ-Chetak_TotalDistance) VALUES ('"+player_uuid+"','22-05-2021 21:41:04','0','0','1000','50','50','0','1','80','85','12','180','1','0','0','50','85','15','120','1','0');")
    conn.execute(quer)
    query = conn.execute("select * from PlayerData where ID=?;",(player_uuid,))
    result = {'data': [dict(zip(tuple (query.keys()) ,i)) for i in query.cursor]}
    print("Successfully updated default values in db")
        
    return jsonify(result)


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

@app.route('/get_weather')
def get_weather():
    a=[]
    with open('CurrentWeather.xml', 'r') as f:
        a = f.read()
    f.closed
    # print(a) #To see the complete weather xml which is read from the original openmaps server xml file
    response = make_response(a)                                           
    response.headers['Content-Type'] = 'application/xml; charset=utf-8'
    return response



########## API JWT TOKEN #############

def token_required(f):
    @wraps(f)
    def decorated(*args, **kwargs):
        token = request.args.get('token') #http://127.0.0.1:8000/route?token=alshfjfjdklsfj89549834ur

        if not token:
            return jsonify({'message' : 'Token is missing!'}), 403

        try: 
            data = jwt.decode(token, app.config['SECRET_KEY'])
        except:
            return jsonify({'message' : 'Token is invalid!'}), 403

        return f(*args, **kwargs)

    return decorated

@app.route('/unprotected')
def unprotected():
    return jsonify({'message' : 'Anyone can view this!'})

@app.route('/protected')
@token_required
def protected():
    return jsonify({'message' : 'This is only available for people with valid tokens.'})

@app.route('/login/<string:player_id>')
def login(player_id):

    conn = e.connect()
    query = conn.execute("select * from PlayerData where ID=?;",(player_id,))
    resultTuple = [dict(zip(tuple (query.keys()) ,i)) for i in query.cursor]

    if len(resultTuple) == 0:
        return make_response('User not found in Database!', 401)
    else:
        token = jwt.encode({'user' : player_id, 'exp' : datetime.datetime.utcnow() + datetime.timedelta(minutes=60)}, app.config['SECRET_KEY'])
        return jsonify({'token' : token.decode('UTF-8')})

#######################################


if __name__ == "__main__":
    from waitress import serve
    serve(app, host="0.0.0.0", port=8000)
