﻿using System;

namespace TwitterClient.Streaming.Exceptions
{
    [Serializable]
    public class RequiredFilterParametersMissingException : Exception
    {
        public RequiredFilterParametersMissingException() : 
            base(@"At least one predicate parameter (follow, locations, or track) must be specified.
                  For more information: https://dev.twitter.com/streaming/reference/post/statuses/filter")
        { }
    }
}
