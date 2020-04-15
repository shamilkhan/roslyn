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
    class ChainCheck
    {
        private const string DiagnosticId = "ChainCheck";
        private const string Title = "ChainCheck";
        private const string MessageFormat = "ChainCheck";
        private const string Category = "Syntax";

        public static DiagnosticDescriptor Rule = new DiagnosticDescriptor(DiagnosticId, Title,
           MessageFormat, Category, DiagnosticSeverity.Warning, isEnabledByDefault: true);

        public static void Analyze(SyntaxNodeAnalysisContext context)
        {
            var exp = context.Node as MemberAccessExpressionSyntax;

            var dotToken = exp.ChildTokens().First(d => d.IsKind(SyntaxKind.DotToken));

            if (dotToken == null) return;

            var hasArgumetList = exp.Parent.ChildNodes().Any(n => n.IsKind(SyntaxKind.ArgumentList));

            if (!hasArgumetList) return;

            if(!dotToken.HasLeadingTrivia)
            {
                var diagnostic = Diagnostic.Create(
                Rule,
                exp.GetLocation(),
                exp.GetText()
                );

                context.ReportDiagnostic(diagnostic);
            }
           
        }
    }
}
