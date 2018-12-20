"use strict";
var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : new P(function (resolve) { resolve(result.value); }).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
Object.defineProperty(exports, "__esModule", { value: true });
const tl = require("azure-pipelines-task-lib/task");
function run() {
    return __awaiter(this, void 0, void 0, function* () {
        try {
            let tool;
            var projectpath = tl.getInput('projectpath', true);
            var startupprojectpath = tl.getInput('startupprojectpath', false);
            var targetfolder = tl.getInput('targetfolder', true);
            var databasecontexts = tl.getDelimitedInput("databasecontexts", "\n", true);
            console.log("Project path: " + projectpath);
            if (startupprojectpath) {
                console.log("Start-up project path: " + startupprojectpath);
            }
            else {
                console.log("Start-up project path not provided. Will use project path instead: " + projectpath);
                startupprojectpath = projectpath;
            }
            console.log("Target folder: " + targetfolder);
            console.log("Number of database contexts: " + databasecontexts.length);
            for (var databasecontext of databasecontexts) {
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
                    .arg(startupprojectpath)
                    .arg('--output')
                    .arg(targetfolder + '/' + databasecontext + '.sql')
                    .arg('--context')
                    .arg(databasecontext);
                yield tool.exec();
            }
            console.log('Generating migration script completed!');
        }
        catch (err) {
            tl.setResult(tl.TaskResult.Failed, err.message);
        }
    });
}
run();
