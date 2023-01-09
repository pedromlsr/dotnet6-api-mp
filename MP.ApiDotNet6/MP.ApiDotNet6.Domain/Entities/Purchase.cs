using MP.ApiDotNet6.Domain.Validations;

namespace MP.ApiDotNet6.Domain.Entities
{
    public sealed class Purchase
    {
        public int Id { get; private set; }
        public int ProductId { get; private set; }
        public int PersonId { get; private set; }
        public DateTime Date { get; private set; }
        public Person Person{ get; private set; }
        public Product Product{ get; private set; }

        public Purchase(int personId, int productId)
        {
            Validation(personId, productId);
        }

        public Purchase(int id, int personId, int productId)
        {
            DomainValidationException.When(id <= 0, "Id da compra deve ser informado!");

            Id = id;

            Validation(personId, productId);
        }

        public void Edit(int id, int personId, int productId)
        {
            DomainValidationException.When(id <= 0, "Id da compra deve ser informado!");

            Id = id;

            Validation(personId, productId);
        }

        private void Validation(int personId, int productId)
        {
            DomainValidationException.When(personId <= 0, "Id da pessoa deve ser informado!");
            DomainValidationException.When(productId <= 0, "Id do produto deve ser informado!");

            PersonId = personId;
            ProductId = productId;
            Date = DateTime.Now;
        }
    }
}
