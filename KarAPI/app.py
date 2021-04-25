from flask import Flask, request
from flask_restful import Resource, Api
from sqlalchemy import create_engine
from json import dumps
import json
from decimal import Decimal

# Assuming KarDB.db is in your app root folder
e = create_engine('sqlite:///KarDB.db')  # loads db into memory

app = Flask(__name__)
api = Api(app)  # api is a collection of objects, where each object contains a specific functionality (GET, POST, etc)

class Petrolbunks_Meta(Resource):
	def get(self):
		conn = e.connect()  # open connection to memory data
		query = conn.execute("select distinct name from PetrolBunks")  # query
		return {'name': [i[0] for i in query.cursor.fetchall()]}  # format results in dict format

class Petrolbunk_Data(Resource):
    def get(self, bunk_id):  # param is pulled from url string
        conn = e.connect()
        query = conn.execute("select * from PetrolBunks where id='%d'"%bunk_id)
        result = {'data': [dict(zip(tuple (query.keys()) ,i)) for i in query.cursor]}
        return result

class multiply(Resource):
    '''dummy function to test apis'''
    def get(self, number):  # param must match uri identifier
        return number * 2

class fuelDeductCheck(Resource):
    def get(self, bunk_id, number):  # param must match uri identifier
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


        return jsonconvert

class poster(Resource):
    def post(self, name, location, litres):
        conn = e.connect()
        query = conn.execute()

class home(Resource):
    def get(self):
        return 'Welcome to KARGAME'

# once we've defined our api functionalities, add them to the master API object
api.add_resource(Petrolbunks_Meta, '/petrolbunks')  # bind url identifier to class
api.add_resource(Petrolbunk_Data, '/bunks/<int:bunk_id>')  # bind url identifier to class; also make it querable
api.add_resource(multiply, '/multiply/<int:number>')  # whatever the number is, multiply by 2
api.add_resource(fuelDeductCheck, '/fuelCheck/<int:bunk_id>/<float:number>')  # pass petrol to be deducted from bunk as float
api.add_resource(home,'/')

if __name__ == '__main__':
    app.run(debug=True)


