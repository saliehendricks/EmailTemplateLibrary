// This file is adapted the from Hangfire library.
// Author Attribution: Sergey Odinokov.
// License along with Hangfire. See <http://www.gnu.org/licenses/>.

namespace EmailTemplateLibrary.Dashboard
{
    public class NonEscapedString
    {
        private readonly string _value;

        public NonEscapedString(string value)
        {
            _value = value;
        }

        public override string ToString()
        {
            return _value;
        }
    }
}
