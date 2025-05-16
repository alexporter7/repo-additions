import os
import sys
import zipfile

BASE_PATH = "/home/alex/RiderProjects/RepoAdditions/"

NAME = sys.argv[1] + "/"
PACK_FILES = ["manifest.json", "README.md", "bin/Debug/netstandard2.1/RepoToast.dll"]

packZip = zipfile.ZipFile(BASE_PATH + NAME + NAME.replace("/","") + ".zip", 'w')
for packFileName in PACK_FILES:
    packFile = BASE_PATH + NAME + packFileName
    if os.path.exists(packFile):
        packZip.write(packFile, os.path.basename(packFile))
    else:
        print(f"File not found: {packFile}")
