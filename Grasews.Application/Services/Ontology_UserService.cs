using Grasews.Domain.Entities;
using Grasews.Domain.Interfaces.Events;
using Grasews.Domain.Interfaces.Repositories;
using Grasews.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Grasews.Application.Services
{
    public class Ontology_UserService : BaseService, IOntology_UserService
    {
        #region Private vars

        private readonly IEventDispatcher _eventDispatcher;
        private readonly IOntology_UserEntityRepository _ontology_UserRepository;
        private readonly IOntologyTermEntityRepository _ontologyTermEntityRepository;
        private readonly IServiceDescription_OntologyEntityRepository _serviceDescription_OntologyEntityRepository;
        private readonly IServiceDescription_UserEntityRepository _serviceDescription_UserEntityRepository;
        private readonly IUserEntityRepository _userRepository;

        #endregion Private vars

        #region Ctors

        public Ontology_UserService(IEventDispatcher eventDispatcher,
            IOntology_UserEntityRepository ontology_UserRepository,
            IUserEntityRepository userRepository,
            IServiceDescription_UserEntityRepository serviceDescription_UserEntityRepository,
            IServiceDescription_OntologyEntityRepository serviceDescription_OntologyEntityRepository,
            IOntologyTermEntityRepository ontologyTermEntityRepository)
        {
            _eventDispatcher = eventDispatcher;
            _ontology_UserRepository = ontology_UserRepository;
            _userRepository = userRepository;
            _serviceDescription_UserEntityRepository = serviceDescription_UserEntityRepository;
            _serviceDescription_OntologyEntityRepository = serviceDescription_OntologyEntityRepository;
            _ontologyTermEntityRepository = ontologyTermEntityRepository;
        }

        #endregion Ctors

        #region IOntology_UserService public methods

        public bool CheckIfOntologyIsAlreadySharedWithUser(int idUser)
        {
            return _ontology_UserRepository.GetAllBySharedUser(idUser).Any();
        }

        public int Create(Ontology_User ontology_User)
        {
            ontology_User.RegistrationDateTime = DateTime.Now;

            _ontology_UserRepository.Create(ontology_User);

            var count = _ontology_UserRepository.SaveChanges();

            return count;
        }

        public int CreateBySharedServiceDescription(int idServiceDescription, int idOntologyTerm)
        {
            var usersWithWhomTheServiceDescriptionIsShared = _serviceDescription_UserEntityRepository.GetAllByServiceDescription(idServiceDescription);

            if (usersWithWhomTheServiceDescriptionIsShared.Any())
            {
                var idOntology = _ontologyTermEntityRepository.Get(idOntologyTerm).IdOntology;

                Ontology_User ontologyUser;

                foreach (var shared in usersWithWhomTheServiceDescriptionIsShared)
                {
                    if (!CheckIfOntologyIsAlreadySharedWithUser(shared.IdSharedUser))
                    {
                        ontologyUser = new Ontology_User { IdOntology = idOntology, IdSharedUser = shared.IdSharedUser };

                        _ontology_UserRepository.Create(ontologyUser);
                    }
                }

                return _ontology_UserRepository.SaveChanges();
            }

            return 0;
        }

        public int CreateBySharedServiceDescription(int idServiceDescription)
        {
            var usersWithWhomTheServiceDescriptionIsShared = _serviceDescription_UserEntityRepository.GetAllByServiceDescription(idServiceDescription);

            Ontology_User ontologyUser;
            List<int> ontologyIds;

            foreach (var shared in usersWithWhomTheServiceDescriptionIsShared)
            {
                ontologyIds = _serviceDescription_OntologyEntityRepository.GetAll()
                   .Where(x => x.IdServiceDescription == idServiceDescription)
                   .Select(x => x.IdOntology).ToList();

                foreach (var idOntology in ontologyIds)
                {
                    ontologyUser = new Ontology_User { IdOntology = idOntology, IdSharedUser = shared.IdSharedUser };

                    _ontology_UserRepository.Create(ontologyUser);
                }
            }

            return _ontology_UserRepository.SaveChanges();
        }

        public Ontology_User Get(int id)
        {
            return _ontology_UserRepository.Get(id);
        }

        public List<Ontology_User> GetAll()
        {
            return _ontology_UserRepository.GetAll().ToList();
        }

        public int Remove(Ontology_User ontology_User)
        {
            _ontology_UserRepository.Remove(ontology_User.Id);

            return _ontology_UserRepository.SaveChanges();
        }

        public int Update(Ontology_User ontology_User)
        {
            _ontology_UserRepository.Update(ontology_User);

            return _ontology_UserRepository.SaveChanges();
        }

        #endregion IOntology_UserService public methods
    }
}