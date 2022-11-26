import sys
import os
import shutil

if __name__ == "__main__":
    #get params
    con = sys.argv[1]
    nettr = sys.argv[2]
    targetinst = sys.argv[3]

    print(targetinst)

    #get path
    nett = nettr[:-1].split("/")[-1]
    print(nett)
    tp = targetinst+"/"+con+"/"+nett

    #crete paths
    if os._exists(tp):
        os.makedirs(tp)

    if os._exists(tp+"/offline"):
        os.makedirs(tp+"/offline")
    if os._exists(tp+"/local"):
        os.makedirs(tp+"/local")
    if os._exists(tp+"/minimal"):
        os.makedirs(tp+"/minimal")
    #minimal
    shutil.copy2("../../../bin/installer-core/"+con+"/installer.exe",tp+"/minimal/installer.exe");

    #local
    shutil.copy2("../../../bin/installer-core/"+con+"/installer.exe",tp+"/local/installer.exe");
    shutil.copy2(sys.argv[4],tp+"/local/1.bin");

    # TODO: ADD OFFLINE BUILD: now offline build copy local
    shutil.copy2("../../../bin/installer-core/"+con+"/installer.exe",tp+"/offline/installer.exe");
    shutil.copy2(sys.argv[4],tp+"/offline/1.bin");



