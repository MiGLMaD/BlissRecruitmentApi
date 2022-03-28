# BlissRecruitmentApi

Requirements
- SQL Server
- .NET CORE 3.1

Instructions

1. BD Setup
- Need to run manually the SQL Script, present in this repo [Here](https://github.com/MiGLMaD/BlissRecruitmentApi/blob/main/BlissRecruitmentApi/SQLScripts/CreateDB.txt) in your SQL Server

2. ConnString configuration
- In the file BlissRecruitmentApi\AppSetings.json [Here](https://github.com/MiGLMaD/BlissRecruitmentApi/blob/main/BlissRecruitmentApi/appsettings.json), change the keyword {SERVER} with the host (Server Name) of your SQL Server

3. Publish
- You can use the instruction: dotnet publish bliss-recruitment-api.sln --self-contained --runtime win-x64 --output publish

4. Running
- Inside the publish folder, you can run this software through the executable BlissRecruitmentApi.exe.
- It will expose 2 endpoints: http://localhost:5000 and https://localhost:5001
- The API specification is available in the https endpoints: https://localhost:5001/swagger
