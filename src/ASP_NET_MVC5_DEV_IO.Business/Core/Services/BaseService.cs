using ASP_NET_MVC5_DEV_IO.Business.Core.Models;
using ASP_NET_MVC5_DEV_IO.Business.Core.Notificacoes;
using FluentValidation;
using FluentValidation.Results;

namespace ASP_NET_MVC5_DEV_IO.Business.Core.Services
{
    public abstract class BaseService
    {
        private readonly INotificador _notificador;

        protected BaseService(INotificador notificador)
        {
            _notificador = notificador; 
        }

        protected void Notificar(ValidationResult validationResult)
        {
            foreach (var erro in validationResult.Errors)
                Notificar(erro.ErrorMessage);
        }

        protected void Notificar(string mensagem)
        {
            _notificador.Handle(new Notificacao(mensagem));
        }
        protected bool ExecutarValidacao<TEntity, TEntityValidator>(TEntity entity, TEntityValidator entityValidator)
            where TEntity : Entity 
            where TEntityValidator : AbstractValidator<TEntity>
        {
            var validator  = entityValidator.Validate(entity);

            if (validator.IsValid) return true; 
            
            Notificar(validator);

            return false; 
        }
    }
}
