1) tizen version
2) tizen list web-project
-----------------------------------------------------
Starting web-project
-----------------------------------------------------
[PROFILE]            [TEMPLATE]
mobile-2.3           WebSinglePageApplication
mobile-2.3           WebBasicApplication
mobile-2.3           WebNavigationApplicati

3) tizen create web-project -n testProject -p mobile-2.4 -t WebBasicApplication -- ~/workspace/
To create a tizen web project ‘tizen create’ command is used with the parameters:

-p            Profile name.

-t             Template name.

-n            Project name.

--             Destination directory
4)  tizen create web-project -n testProject -p tv-5.0 -t WebBasicapplication -- /home/tizen/projects


5) tizen build-web -- /home/tizen/projects/testProject

6) tizen clean -- ~/workspace/testProject

7) ‘certificate’ command generates a Tizen certificate for the application. To upload the application to the Tizen store or install the application to a Tizen device, Tizen certificate must be generated.

-a alias name*            -p password*             -c country code         -s state     -ct  city

-o organization          -u unit                         -n username               -e email    -f    filename*          

-- output path

(*) marked parameters are mandatory.

Example Command:

tizen certificate -a certAuthor -f certFile -p 12345

8) Tizen packages an application with signing profile.

‘security-profiles’ command manages security profiles, which is a set of signing certificates (author certificate, distributor certificate). Sub commands for tizen security profiles are Add, list and remove.
Security-profiles Add command takes the parameters:
-n profile name         -a author certificate path           -p password            

                                    -d distributor certificate path   -dp distributor password

Example Command:

$ tizen security-profiles add -n ProName -a ~/cli-data/keystore/author/certFile.p12 
-p 12345

9) tizen security-profiles list

10) Tizen web application is packed as .wgt file. Package is used to install, uninstall or update application.  Package is zip archive file, and it includes source codes of application and signature data. To publish application in the app store, application should be packed inside .wgt file.

tizen package command takes following parameters:

-t             Package type

-s            Security profile name for signing.

--             Build output path

Example Command:

~$ tizen package -t wgt -s ProName -- ~/workspace/testProject

11) tizen manual [<command>]
You can use all CLI commands to as <command>:

cli-config, list, create, build-native, build-web, clean, certificate, security-profiles, package, install, run, uninstall, version
