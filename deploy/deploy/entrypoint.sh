#!/bin/bash

sdb connect $TV_IP

cat <<EOF >/home/tizen/tizen-studio-data/profile/profiles.xml
<?xml version="1.0" encoding="UTF-8"?>
<profiles active="ea_cert" version="3.1">
   <profile name="ea_cert">
      <profileitem ca="" distributor="0" key="/home/tizen/SamsungCertificate/ea_cert/author.p12" password="test1test" rootca="" />
      <profileitem ca="" distributor="1" key="/home/tizen/SamsungCertificate/ea_cert/distributor.p12" password="test1test" rootca="" />
      <profileitem ca="" distributor="2" key="" password="" rootca="" />
   </profile>
</profiles>
EOF


tizen package --type wgt --sign ea_cert -- /home/tizen/projects/testProject

tizen install --name /home/tizen/projects/testProject/testProject.wgt -s $TV_IP:26101

