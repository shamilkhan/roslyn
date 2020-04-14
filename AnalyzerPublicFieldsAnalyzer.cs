using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

namespace AnalyzerPublicFields
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class AnalyzerPublicFieldsAnalyzer : DiagnosticAnalyzer
    {
        public const string DiagnosticId = "PublicField";
        private const string Title = "Filed is public!!!";
        private const string MessageFormat = "Field '{0}' is public 111";
        private const string Category = "Syntax";

        private static DiagnosticDescriptor Rule = new DiagnosticDescriptor(DiagnosticId, Title, 
            MessageFormat, Category, DiagnosticSeverity.Warning, isEnabledByDefault: true);


        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics
        {
            get
            {
                return ImmutableArray.Create(
                    BooleanPropsNameAnalyzer.Rule, 
                    SimpleInterfaceAnalizer.Rule,
                    OneFileOneClass.Rule
                  );
            }
        }

        public override void Initialize(AnalysisContext context)
        {
            context.RegisterSyntaxNodeAction(OneFileOneClass.Analyze, SyntaxKind.NamespaceDeclaration);
            context.RegisterSyntaxNodeAction(BooleanPropsNameAnalyzer.Analyze, SyntaxKind.PropertyDeclaration);
            context.RegisterSyntaxNodeAction(SimpleInterfaceAnalizer.Analyze, SyntaxKind.InterfaceDeclaration);
        }

    }
}
