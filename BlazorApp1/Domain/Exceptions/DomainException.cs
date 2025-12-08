using System;

namespace BlazorApp1.Domain.Exceptions
{
    /// <summary>
    /// 도메인 규칙 위반을 나타내는 기본 예외 클래스
    /// </summary>
    public class DomainException : Exception
    {
        public DomainException() { }
        public DomainException(string message) : base(message) { }
        public DomainException(string message, Exception innerException) : base(message, innerException) { }

        public class ValidationException : DomainException
        {
            public ValidationException(string message) : base(message) { }
        }
    }
}
