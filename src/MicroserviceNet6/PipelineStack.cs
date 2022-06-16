using Amazon.CDK;
using Amazon.CDK.AWS.CodeCommit;
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
        }
    }
}