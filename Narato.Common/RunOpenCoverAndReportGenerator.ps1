cd $env:USERPROFILE\Source\Repos\narato-common\Narato.Common\tests\Narato.Common.Test
&"$env:USERPROFILE\.nuget\packages\OpenCover\4.6.519\tools\OpenCover.Console.exe" "-target:${env:programfiles}\dotnet\dotnet.exe" "-targetargs: test" "-register:User" "-filter:+[Narato.Common*]* -[*.Test]*" "-output:Unit_Test_Coverage_Report.xml" -oldStyle


&"$env:USERPROFILE\.nuget\packages\ReportGenerator\2.4.5\tools\ReportGenerator.exe" -reports:"$env:USERPROFILE\Source\Repos\narato-common\Narato.Common\tests\Narato.Common.Test\Unit_Test_Coverage_Report.xml" -targetdir:"$env:USERPROFILE\Source\Repos\narato-common\Narato.Common\UnitTestCoverage_Report"

cd $env:USERPROFILE\Source\Repos\narato-common\Narato.Common\