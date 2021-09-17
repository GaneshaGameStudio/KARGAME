import requests
import xml.etree.ElementTree as ET
from time import time, sleep
  

apikey = '42d4191ee9409cb50f90142a4630a078'
URL = 'http://api.openweathermap.org/data/2.5/weather?'+'q=Bengaluru,IN@&mode=xml&units=metric&APPID='+apikey

def getWeather():
  try:
    r = requests.get(url = URL)
    print(r.status_code)
    # print(r.content)

    root = ET.fromstring(r.text)
    tree = ET.ElementTree(root)
    tree.write("CurrentWeather.xml")
  except:
    print("Resource temporarily unavailable")
    getWeather()




while True:
    getWeather()
    sleep(600) #Runs every 10min