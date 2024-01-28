using System.Linq.Expressions;
using LinqSpecs.Core;
using TimLearning.Domain.Entities;

namespace TimLearning.Application.Specifications.Dynamic.Users;

public class UserByEmailSpecification : Specification<User>
{
    private readonly string _email;

    public UserByEmailSpecification(string email)
    {
        _email = email;
    }

    public override Expression<Func<User, bool>> ToExpression()
    {
        return u => u.Email == _email;
    }
}
