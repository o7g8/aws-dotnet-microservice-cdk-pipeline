using Amazon.CDK;
using Amazon.CDK.AWS.CodeCommit;
using Amazon.CDK.AWS.CodePipeline;
using Amazon.CDK.AWS.CodePipeline.Actions;
using Amazon.CDK.Pipelines;
using Constructs;

namespace MicroserviceNet6
{
    public class MicroservicePipelineStack : Stack
    {
        public MicroservicePipelineStack(Construct parent, string id, IStackProps props = null) : base(parent, id, props)
        {
            // Creates a CodeCommit repository called 'MicroserviceRepo'
            var repo = new Repository(this, "MicroserviceRepo", new RepositoryProps
            {
                RepositoryName = "MicroserviceRepo"
            });

                       // The basic pipeline declaration. This sets the initial structure
            // of our pipeline
            var pipeline = new CodePipeline(this, "Pipeline", new CodePipelineProps
            {
                PipelineName = "MicroservicePipeline",

                // Builds our source code outlined above into a could assembly artifact
                Synth = new ShellStep("Synth", new ShellStepProps{
                    Input = CodePipelineSource.CodeCommit(repo, "master"),  // Where to get source code to build
                    InstallCommands = new string[] {
                        /*
                        "npm install -g aws-cdk",
                        "wget https://packages.microsoft.com/config/ubuntu/18.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb",
                        "sudo dpkg -i packages-microsoft-prod.deb",
                        "rm packages-microsoft-prod.deb",
                        "sudo apt-get update",
                        "sudo apt-get install -y apt-transport-https",
                        "sudo apt-get update",
                        "sudo apt-get install -y dotnet-sdk-6.0",
                        */
                        "/usr/local/bin/dotnet-install.sh --channel LTS",
                        //"sudo apt-get install -y dotnet-sdk-3.1", // Language-specific install cmd
                    },
                    Commands = new string[] {
                        "cd src",
                        "dotnet --version",
                        "dotnet --list-sdks",
                        "dotnet build"  // Language-specific build cmd
                    }
                }),
            });
        }
    }
}