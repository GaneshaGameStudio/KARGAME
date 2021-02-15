import csv
import os
rows = 36
columns = 36
with open('BusRoutes.csv', 'w') as myfile:
    for i in range(0,rows):
        for j in range(0,columns):
            myfile.write(str(i)+','+str(j)+',\n')