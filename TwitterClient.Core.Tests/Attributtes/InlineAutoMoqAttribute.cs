using System.Collections.Generic;
using Ploeh.AutoFixture.Xunit;
using Xunit.Extensions;

namespace TwitterClient.Core.Tests.Attributtes
{
    class InlineAutoMoqAttribute : CompositeDataAttribute
    {
        public IEnumerable<object> Values { get; }

        public AutoDataAttribute AutoMoqDataAttribute { get; }

        public InlineAutoMoqAttribute(params object[] values)
        : this(new AutoMoqDataAttribute(), values)
        {
        }

        public InlineAutoMoqAttribute(AutoMoqDataAttribute autoMoqDataAttribute, params object[] values)
        : base(new InlineAutoMoqAttribute(values) as DataAttribute, autoMoqDataAttribute as DataAttribute)
        {
            AutoMoqDataAttribute = autoMoqDataAttribute;
            Values = values;
        }
    }
}
