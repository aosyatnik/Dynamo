using System;
using System.Collections.Generic;
using DSCore;
using Dynamo.Graph.Nodes;
using ProtoCore.AST.AssociativeAST;

namespace CoreNodeModels
{
    [IsDesignScriptCompatible]
    [NodeName("MyTestNode")]
    [NodeCategory("Core")]
    [InPortNames("inPort")]
    [InPortTypes("int")]
    [OutPortNames("outPort")]
    [OutPortTypes("int")]
    public class MyTestNode : NodeModel
    {
        private SomethingImportant I_Want_Process_It = new SomethingImportant(10);

        public MyTestNode()
        {
            RegisterAllPorts();
        }

        public override IEnumerable<AssociativeNode> BuildOutputAst(List<AssociativeNode> inputAstNodes)
        {
            // This won't work.
            // Ok got it.
            /*var functionCall =
               AstFactory.BuildFunctionCall(new Func<int, SomethingImportant>(I_Want_To_Call_This),
                   new List<AssociativeNode>() { inputAstNodes[0] });*/

            // But this will work.
            var functionCall2 =
               AstFactory.BuildFunctionCall(new Func<SomethingImportant, int>(DummyClass2.But_Have_To_Call_This),

                // How to send SomethingImportant (I want to send I_Want_Process_It)?
                // I don't want to all AstFactory.BuildIntNode
                   new List<AssociativeNode>() { inputAstNodes[0] });

            return new[]
            {
                AstFactory.BuildAssignment(GetAstIdentifierForOutputIndex(0), /*functionCall*/functionCall2),
            };
        }
    }
}
