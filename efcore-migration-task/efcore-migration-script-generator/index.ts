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
        
        var idempotent : boolean = true;
        if(tl.filePathSupplied('idempotent')) {
            idempotent = tl.getBoolInput('idempotent', false);
        }

        var build : boolean = true;
        if(tl.filePathSupplied('build')) {
            build = tl.getBoolInput('build', false);
        }

        var workingDirectory : undefined|string = undefined;
        if(tl.filePathSupplied('workingDirectory')) {
            workingDirectory = tl.getPathInput('workingDirectory', false);
        }

        var configuration : undefined|string = undefined;
        if(tl.filePathSupplied('configuration')) {
            configuration = tl.getPathInput('configuration', false);
        }

        var nugetconfiguration : undefined|string = undefined;
        if(tl.filePathSupplied('nugetconfiguration')) {
            nugetconfiguration = tl.getPathInput('nugetconfiguration', false);
        }

        var installdependencies : boolean = false;
        if(tl.filePathSupplied('installdependencies')) {
            installdependencies = tl.getBoolInput('installdependencies', false);
        }

        var eftoolversion : undefined|string = undefined;
        if(tl.filePathSupplied('eftoolversion')) {
            eftoolversion = tl.getPathInput('eftoolversion', false);
        }

        var runtime : undefined|string = undefined;
        if(tl.filePathSupplied('runtime')) {
            runtime = tl.getPathInput('runtime', false);
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
                output += data.toString();
            });

            await tool.exec();

            console.log("Parsing output: \"" + output + "\"");

            // if(output.indexOf("\ndotnet-ef ") < 0) {
            if(/\sdotnet-ef\s/gm.test(output) === false) {                

                if(eftoolversion) {
                    console.log("Installing latest version of dotnet-ef as global tool.");
                }
                else {
                    console.log("Installing version " + eftoolversion + " of dotnet-ef as global tool.");
                }

                let cmdPath = tl.which('dotnet');
                tool = tl.tool(cmdPath)
                            .arg('tool')
                            .arg('install')
                            .arg('--global')
                            .arg('dotnet-ef');

                if(eftoolversion) {
                    tool = tool.arg('--version')
                                .arg(eftoolversion);
                }

                if(nugetconfiguration) {
                    console.log("Will use NuGet configuration file: " + nugetconfiguration);
                    tool = tool.arg('--configfile')
                                .arg(nugetconfiguration);
                }

                await tool.exec();
            }
            else {
                console.log("dotnet-ef is already installed.");
            }
        } else {
            console.log("Will not try to install dotnet-ef. If you are using .NET Core 3 you could enable 'Install dependencies for .NET Core 3'");
            console.log("to do this automatically. If you are using .NET Core 2 you may need to add the 'Use .NET Core' before running this task.");
            console.log("See here for more details: https://github.com/pekspro/EF-Migrations-Script-Generator-Task");
        }

        for(var databasecontext of databasecontexts)
        {
            console.log("Generating migration script for " + databasecontext + " in project " + projectpath);

            let cmdPath = tl.which('dotnet');
            tool = tl.tool(cmdPath)
                        .arg('ef')
                        .arg('migrations')
                        .arg('script')
                        .arg('--project')
                        .arg(projectpath as string)
                        .arg('--startup-project')
                        .arg(startupprojectpath as string)
                        .arg('--output')
                        .arg(targetfolder + '/' + databasecontext + '.sql')
                        .arg('--context')
                        .arg(databasecontext)
                        .arg('--verbose');

            if(build) {
                console.log("Projects will be built before scripts are generated.");
            } else {
                console.log("Projects will not be built before scripts are generated.");
                tool = tool.arg('--no-build');
            }

            if(configuration) {
                console.log("'" + configuration + "' build configuration will be used.");
                tool = tool.arg('--configuration').arg(configuration);
            } else {
                console.log("Default build configuration will be used.");
            }

            if(idempotent) {
                console.log("The script will be idempotent.");
                tool = tool.arg('--idempotent');
            } else {
                console.log("The script will not idempotent.");
            }

            if(runtime) {
                console.log("The restore target is '" + runtime + "'");
                tool = tool.arg('--runtime ' + runtime);
            }
            
            if(workingDirectory) {
                console.log("Will use working directory: " + workingDirectory);

                await tool.exec(<trm.IExecOptions>{
                    cwd: workingDirectory
                });

            } else {
                await tool.exec();
            }
            
        }
       
        console.log('Generating migration script completed!');
    }
    catch (err) {
        tl.setResult(tl.TaskResult.Failed, err.message);
    }
}

run();