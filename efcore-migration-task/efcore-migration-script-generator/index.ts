import tl = require('azure-pipelines-task-lib/task');
import trm = require('azure-pipelines-task-lib/toolrunner');

async function run() {
    try {
        let tool: trm.ToolRunner;

        var projectpath = tl.getInput('projectpath', true);
        var targetfolder = tl.getInput('targetfolder', true);
        var databasecontexts = tl.getDelimitedInput("databasecontexts", "\n", true);

        console.log("Project path: " + projectpath);
        console.log("Target folder: " + targetfolder);
        console.log("Number of database contexts: " + databasecontexts.length);

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
                        .arg(projectpath)
                        .arg('--startup-project')
                        .arg(projectpath)
                        .arg('--output')
                        .arg(targetfolder + '/' + databasecontext + '.sql')
                        .arg('--context')
                        .arg(databasecontext);
    
            await tool.exec();
        }
       
        console.log('Generating migration script completed!');
    }
    catch (err) {
        tl.setResult(tl.TaskResult.Failed, err.message);
    }
}

run();