using System.ComponentModel.DataAnnotations;

namespace BlazorApp1.Domain.ValueObject
{
    public record Email
    {
        public string Value { get; }

        public Email(string value)
        {
            if (string.IsNullOrWhiteSpace(value) || !value.Contains("@"))
                throw new ValidationException("이메일은 비워둘 수 없습니다.");

            Value = value;
        }

        public override string ToString() => Value;
    }
}
