using BlazorApp1.Domain.ValueObject;
using System.ComponentModel.DataAnnotations;

namespace BlazorApp1.Domain.Entity
{
    public class User
    {
        public int Id { get; private set; }

        public string Name { get; private set; }
        public Email Email { get; private set; }

        public User(int id, string name, Email email)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ValidationException("사용자 이름은 비워둘 수 없습니다.");

            Id = id;
            Name = name;
            Email = email ?? throw new ValidationException("이메일은 필수입니다.");
        }
    }
}
