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
                        "npm install -g aws-cdk",
                        "/usr/local/bin/dotnet-install.sh --channel LTS",
                    },
                    Commands = new string[] {
                        "cd src && dotnet build",
                        "cd .. && cdk synth"
                    }
                }),
            });

            var deploy = new MicroservicePipelineStage(this, "Deploy");
            var deployStage = pipeline.AddStage(deploy);
        }
    }
}