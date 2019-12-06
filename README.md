README for Mismatchr
A permission mismatch analysis engine for Android developers and researchers alike

What have you submitted (list of files, formats):
The codebase for Mismatchr is in C#, and is most easily run if opened in Visual Studio from the solution file.
The dataset for Mismatchr is xml Covert output files that can be found in just_xml_files
Most program logic is found in AppPermissions.cs, ParseXML.cs, and Permission.cs. The Values Controller returns the modified dataset to the frontend.

How to compile/run the programs
Once the project is open in Visual Studio, press run, and the browser will open the analysis page.
If you want to analyze a home-built app, run the Covert tool on your application and add the xml output from the model or merged folder to the folder just_xml_files in our app.

Any limitations/problems that are known
N/A

Any dependencies that are required to compile/run code or read diagrams
Visual Studio, dotnet core