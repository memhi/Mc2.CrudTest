using System;
using System.Collections.Specialized;
using System.Web;

namespace Mc2.CrudTest.Presentation.Front.Util
{
    public class UriBuilderExt
    {
        private readonly NameValueCollection _collection;
        private readonly UriBuilder _builder;

        public UriBuilderExt(Uri uri)
        {
            _builder = new UriBuilder(uri);
            _collection = HttpUtility.ParseQueryString(string.Empty);
        }

        public UriBuilderExt AddParameter(string key, string value)
        {
            _collection.Add(key, value);
            return this;
        }

        public Uri Uri
        {
            get
            {
                _builder.Query = _collection.ToString();
                return _builder.Uri;
            }
        }
    }
}
