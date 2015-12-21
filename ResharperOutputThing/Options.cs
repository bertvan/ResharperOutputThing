using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandLine;
using CommandLine.Text;

namespace ResharperOutputThing
{
    public class Options
    {
        [ParserState]
        public IParserState LastParserState { get; set; }

        [Option(Required=true)]
        public FilterStatus FilterStatus { get; set; }

        [HelpOption]
        public string GetUsage()
        {
            var help = new HelpText();

            if (this.LastParserState.Errors.Any())
            {
                var errors = help.RenderParsingErrorsText(this, 2);

                if (!string.IsNullOrEmpty(errors))
                {
                    help.AddPreOptionsLine(string.Concat(Environment.NewLine, "ERROR(S):"));
                    help.AddPreOptionsLine(errors);
                }
            }

            return help;
        }
    }

    public enum FilterStatus
    {
        Unknown,
        Success,
        Failed
    }
}
