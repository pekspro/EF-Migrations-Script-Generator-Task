import tl = require('azure-pipelines-task-lib/task');
import trm = require('azure-pipelines-task-lib/toolrunner');

async function run() {
    try {
        let tool: trm.ToolRunner;

        var projectpath = tl.getPathInput('projectpath', true);
        var startupprojectpath : undefined|string = undefined;
        if(tl.filePathSupplied('startupprojectpath')) {
            startupprojectpath = tl.getPathInput('startupprojectpath', false);
        }
        var targetfolder = tl.getInput('targetfolder', true);
        var databasecontexts = tl.getDelimitedInput("databasecontexts", "\n", true);
        
        var installdependencies : boolean = false;
        if(tl.filePathSupplied('installdependencies')) {
            installdependencies = tl.getBoolInput('installdependencies', false);
        }

        console.log("Project path: " + projectpath);
        
        

        if(startupprojectpath) {
            console.log("Start-up project path: " + startupprojectpath);
        } else {
            console.log("Start-up project path not provided. Will use project path instead: " + projectpath);
            startupprojectpath = projectpath;
        }
        console.log("Target folder: " + targetfolder);
        console.log("Number of database contexts: " + databasecontexts.length);

        
        if(installdependencies) {

            console.log("Checking of dotnet-ef is installed.");

            var output = '';

            let cmdPathCheck = tl.which('dotnet');
            tool = tl.tool(cmdPathCheck)
                        .arg('tool')
                        .arg('list')
                        .arg('--global');
            
            tool.on('stdout', (data) => {
                output = data.toString();
            });

            await tool.exec();

            if(output.indexOf("\ndotnet-ef ") < 0) {
                console.log("Installing dotnet-ef as global tool.");

                let cmdPath = tl.which('dotnet');
                tool = tl.tool(cmdPath)
                            .arg('tool')
                            .arg('install')
                            .arg('--global')
                            .arg('dotnet-ef')
                            .arg('--version')
                            .arg('3.0.0-*');
        
                await tool.exec();
            }
            else {
                console.log("dotnet-ef is already installed.");
            }
        } else {
            console.log("Will not try to install dotnet-ef. If you are using .NET Core 3 you could enable 'Install dependencies for .NET Core 3' to do this automatically.");
        }

        for(var databasecontext of databasecontexts)
        {
            console.log("Generating migration script for " + databasecontext + " in project " + projectpath);

            let cmdPath = tl.which('dotnet');
            tool = tl.tool(cmdPath)
                        .arg('ef')
                        .arg('migrations')
                        .arg('script')
                        .arg('--idempotent')
                        .arg('--project')
                        .arg(projectpath as string)
                        .arg('--startup-project')
                        .arg(startupprojectpath as string)
                        .arg('--output')
                        .arg(targetfolder + '/' + databasecontext + '.sql')
                        .arg('--context')
                        .arg(databasecontext)
                        .arg('--verbose');
    
            await tool.exec();
        }
       
        console.log('Generating migration script completed!');
    }
    catch (err) {
        tl.setResult(tl.TaskResult.Failed, err.message);
    }
}

run();
