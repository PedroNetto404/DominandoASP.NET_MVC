using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASP_NET_MVC5_DEV_IO.Business.Core.Models
{
    public abstract class Entity //Identifica o tipo de um objeto entidade do negócio 
    {                            //Uma entidade pode ser identificada por um valor único
        public Guid Id { get; set; }

        protected Entity()
        {
            Id = Guid.NewGuid();
        }
    }
}
