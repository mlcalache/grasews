using Grasews.Application.DTOs;
using Grasews.Domain.Entities;
using Grasews.Domain.Events;
using Grasews.Domain.Interfaces.DTOs;
using Grasews.Domain.Interfaces.Events;
using Grasews.Domain.Interfaces.Repositories;
using Grasews.Domain.Interfaces.Services;
using Grasews.Infra.CrossCutting.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Grasews.Application.Services
{
    public class OntologyService : BaseService, IOntologyService
    {
        #region Private vars

        private readonly IOntology_UserEntityRepository _ontology_UserRepository;
        private readonly IOntologyEntityRepository _ontologyRepository;
        private readonly IOntologyTermEntityRepository _ontologyTermRepository;
        private readonly IServiceDescription_UserEntityRepository _serviceDescription_UserRepository;

        private readonly XNamespace owlNamespace = XNamespace.Get("http://www.w3.org/2002/07/owl#");
        private readonly XNamespace rdfNamespace = XNamespace.Get("http://www.w3.org/1999/02/22-rdf-syntax-ns#");
        private readonly XNamespace rdfsNamespace = XNamespace.Get("http://www.w3.org/2000/01/rdf-schema#");
        private readonly XNamespace xmlNamespace = XNamespace.Get("http://www.w3.org/XML/1998/namespace");
        private readonly XNamespace xsdNamespace = XNamespace.Get("http://www.w3.org/2001/XMLSchema#");

        private readonly IEventDispatcher _eventDispatcher;

        #endregion Private vars

        #region Ctors

        /// <summary>
        ///
        /// </summary>
        /// <param name="ontologyRepository"></param>
        /// <param name="ontologyTermRepository"></param>
        /// <param name="ontology_UserRepository"></param>
        public OntologyService(IOntologyEntityRepository ontologyRepository,
            IOntologyTermEntityRepository ontologyTermRepository,
            IOntology_UserEntityRepository ontology_UserRepository,
            IServiceDescription_UserEntityRepository serviceDescription_UserRepository,
            IEventDispatcher eventDispatcher)
        {
            _ontologyRepository = ontologyRepository;
            _ontologyTermRepository = ontologyTermRepository;
            _ontology_UserRepository = ontology_UserRepository;
            _serviceDescription_UserRepository = serviceDescription_UserRepository;
            _eventDispatcher = eventDispatcher;
        }

        #endregion Ctors

        #region IOntologyService public methods

        /// <summary>
        ///
        /// </summary>
        /// <param name="idOntology"></param>
        /// <param name="idSharedUsers"></param>
        /// <returns></returns>
        public int AddOntologyToSharedUsers(int idOntology, IEnumerable<int> idSharedUsers)
        {
            foreach (var idUser in idSharedUsers)
            {
                _ontology_UserRepository.Create(new Ontology_User { IdOntology = idOntology, IdSharedUser = idUser });
            }

            return _ontology_UserRepository.SaveChanges();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="idOntology"></param>
        /// <param name="idServiceDescription"></param>
        public void AddOntologyToSharedUsersAsync(int idOntology, int idServiceDescription)
        {
            _eventDispatcher.Dispatch(new AddOntologyToSharedUsersEvent(idOntology, idServiceDescription));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idOntology"></param>
        /// <param name="idServiceDescription"></param>
        public void AddOntologyToSharedUsersSync(int idOntology, int idServiceDescription)
        {
            var sharedUsers = _serviceDescription_UserRepository.GetAllByServiceDescription(idServiceDescription).ToList();
            //var sharedUsers = _serviceDescription_UserService.GetAllByServiceDescription(IdServiceDescription);

            var idSharedUsers = sharedUsers.Select(x => x.IdSharedUser);

            AddOntologyToSharedUsers(idOntology, idSharedUsers);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="ontologyName"></param>
        /// <returns></returns>
        public bool CheckIfOntologyAlreadyExists(string ontologyName)
        {
            return _ontologyRepository.GetAll().Any(x => x.OntologyName.ToUpper() == ontologyName.ToUpper());
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="ontology"></param>
        /// <returns></returns>
        public int Create(Ontology ontology)
        {
            ontology.HexColor = ColorHelper.ToHex(ColorHelper.GetRandomColor());

            ontology.RegistrationDateTime = DateTime.Now;

            _ontologyRepository.Create(ontology);

            ParseXml(ontology);

            return _ontologyRepository.SaveChanges();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="ontologyTerm1"></param>
        /// <param name="ontologyTerm2"></param>
        /// <returns></returns>
        public IEnumerable<OntologyTerm> FindPathBetweenTerms(OntologyTerm ontologyTerm1, OntologyTerm ontologyTerm2)
        {
            var path = new List<OntologyTerm>();

            var isTerm1Root = FindTerm(ontologyTerm1.ChildrenOntologyTerms, ontologyTerm2.Id) != null;

            if (isTerm1Root)
            {
                path = TracePath(ontologyTerm1, ontologyTerm2).ToList();
                return path;
            }

            var isTerm2Root = FindTerm(ontologyTerm2.ChildrenOntologyTerms, ontologyTerm1.Id) != null;

            if (isTerm2Root)
            {
                path = TracePath(ontologyTerm2, ontologyTerm1).ToList();
                return path;
            }

            return null;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="idOntology"></param>
        /// <param name="idOntologyTerm"></param>
        /// <returns></returns>
        public OntologyTerm FindTerm(int idOntology, int idOntologyTerm)
        {
            var ontology = GetComplete(idOntology);

            var ontologyTerm = FindTerm(ontology, idOntologyTerm);

            return ontologyTerm;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="ontology"></param>
        /// <param name="idOntologyTerm"></param>
        /// <returns></returns>
        public OntologyTerm FindTerm(Ontology ontology, int idOntologyTerm)
        {
            OntologyTerm termFound = null;

            termFound = FindTerm(ontology.OntologyTerms, idOntologyTerm);

            return termFound;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="idOntology"></param>
        /// <returns></returns>
        public Ontology Get(int idOntology)
        {
            return _ontologyRepository.Get(idOntology);
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Ontology> GetAll()
        {
            return _ontologyRepository.GetAll().ToList();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="idUser"></param>
        /// <returns></returns>
        public IEnumerable<Ontology> GetAllByUser(int idUser)
        {
            var ontologies = new List<Ontology>();

            //Get all ontologies created by the user
            var ownedOntologies = _ontologyRepository.GetAllByUser(idUser).ToList();
            //Get all ontologies shared with the user
            var sharedOntologies = _ontology_UserRepository.GetAllBySharedUser(idUser).Select(x => x.Ontology).ToList();

            //Add both lists of ontologies into a single list
            ontologies.AddRange(ownedOntologies);
            ontologies.AddRange(sharedOntologies);

            return ontologies;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="idOntologyTerms"></param>
        /// <returns></returns>
        public IEnumerable<Ontology> GetAllOntologiesFromOntologyTerms(IEnumerable<int> idOntologyTerms)
        {
            var idOntologies = _ontologyTermRepository.GetAll().Where(x => idOntologyTerms.Contains(x.Id)).Select(x => x.IdOntology).ToList();

            return _ontologyRepository.GetAll().Where(x => idOntologies.Contains(x.Id)).ToList();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="ontologyName"></param>
        /// <returns></returns>
        public Ontology GetByOntologyName(string ontologyName)
        {
            return _ontologyRepository.GetAll().FirstOrDefault(x => x.OntologyName == ontologyName);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="serviceName"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Ontology GetByServiceName(string serviceName, int userId)
        {
            return _ontologyRepository.GetAllByUser(userId).FirstOrDefault(x => x.OntologyName == serviceName);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="idOntology"></param>
        /// <returns></returns>
        public Ontology GetComplete(int idOntology)
        {
            var ontology = _ontologyRepository.Get(idOntology);

            ParseXml(ontology);

            var levels = ontology.OntologyTerms.ToList().Max(x => x.Depth.Count());

            ontology = _ontologyRepository.GetComplete(idOntology, levels);

            return ontology;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="ontology"></param>
        /// <returns></returns>
        public string GetHtmlTreeViewMenu(Ontology ontology)
        {
            var htmlBuilder = new StringBuilder();

            var color = ontology.HexColor;

            string ontologyDisplayName;

            ontologyDisplayName = ontology.OntologyName;

            //if (ontology.OntologyName.Length > 20)
            //{
            //    ontologyDisplayName = ontology.OntologyName.Substring(0, 19) + "...";
            //}

            //Opening LI for Ontology
            htmlBuilder.Append($"<li class='treeview ontologyContextMenu' id='li-ontology-{ontology.Id}' ontology-name='{ontology.OntologyName}' ontology-id='{ontology.Id}'>".Trim());
            htmlBuilder.Append($"   <a href='#'>".Trim());
            htmlBuilder.Append($"       <i class='fa fa-file-code-o'></i> <span>{ontologyDisplayName}&nbsp;</span>".Trim());
            //htmlBuilder.Append($"       <i class='fa fa-circle' style='color: {color};'></i>".Trim());
            htmlBuilder.Append($"       <span class='pull-right-container'>".Trim());
            htmlBuilder.Append($"           <i class='fa fa-eye pull-right ontology-eye text-green'></i>".Trim());
            htmlBuilder.Append($"           &nbsp;&nbsp;<i class='fa fa-angle-right pull-left'></i>".Trim());
            htmlBuilder.Append($"       </span>".Trim());
            htmlBuilder.Append($"   </a>".Trim());

            //Opening UL for Ontology Terms
            htmlBuilder.Append($"   <ul class='treeview-menu' style='display: none;'>".Trim());

            htmlBuilder.Append(CreateHtmlListOfTerms(ontology.Id, ontology.OntologyTerms.Where(x => x.IdParentOntologyTerm == null).ToList(), color));

            //Closing UL for Ontology Terms
            htmlBuilder.Append($"   </ul>".Trim());

            //Closing LI for Ontology
            htmlBuilder.Append($"</li>");

            //createContextMenusForTreeView();

            var html = htmlBuilder.ToString().Trim();

            //html = HtmlHelper.Minify(html);

            return html;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="ontology"></param>
        /// <returns></returns>
        public string GetOntologyNameFromXml(Ontology ontology)
        {
            var owl = XDocument.Parse(ontology.Xml);
            var rootElement = owl.Descendants(rdfNamespace + "RDF").FirstOrDefault();
            var baseAttribute = rootElement.Attributes().FirstOrDefault(x => Equals(x.Name, xmlNamespace + "base"));
            var ontologyURI = baseAttribute.Value;

            var ontologyName = UrlHelper.ExtractOntologyNameFromUrl(ontologyURI);

            return ontologyName;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="idOntologyTerm"></param>
        /// <returns></returns>
        public OntologyTerm GetOntologyTerm(int idOntologyTerm)
        {
            return _ontologyTermRepository.Get(idOntologyTerm);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="parseOwlRequestDTO"></param>
        /// <returns></returns>
        public IParseOwlResponseDTO ParseXml(IParseOwlRequestDTO parseOwlRequestDTO)
        {
            var xDocument = XDocument.Parse(parseOwlRequestDTO.Xml);
            var ontologyXmlNameSpace = xDocument.Descendants(rdfNamespace + "RDF").Select(x => x.Attribute("xmlns").Value).FirstOrDefault();

            if (ontologyXmlNameSpace.IndexOf("#") != -1)
            {
                ontologyXmlNameSpace = ontologyXmlNameSpace.Substring(0, ontologyXmlNameSpace.IndexOf("#"));
            }

            var ontologyTerms = xDocument.Descendants(owlNamespace + "Class")
                .Where(x => x.Attribute(rdfNamespace + "about") != null)
                .Select(x => new OntologyTerm
                {
                    TermRaw = x.ToString(),
                    TermUri = x.Attribute(rdfNamespace + "about").Value,
                    TermName = UrlHelper.ExtractTermIdentifierFromUrl(x.Attribute(rdfNamespace + "about").Value),
                    ParentTermURI = x.Descendants(rdfsNamespace + "subClassOf")?.FirstOrDefault(y => !y.HasElements)?.Attribute(rdfNamespace + "resource").Value
                })
                .ToList();

            ArrangeTerm(ontologyTerms);

            return new ParseOwlResponseDTO { OntologyName = parseOwlRequestDTO.OntologyName, Xml = parseOwlRequestDTO.Xml, OntologyTerms = ontologyTerms };
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="ontology"></param>
        public void ParseXml(Ontology ontology)
        {
            var xDocument = XDocument.Parse(ontology.Xml);
            var ontologyXmlNameSpace = xDocument.Descendants(rdfNamespace + "RDF").Select(x => x.Attribute("xmlns").Value).FirstOrDefault();

            if (ontologyXmlNameSpace.IndexOf("#") != -1)
            {
                ontologyXmlNameSpace = ontologyXmlNameSpace.Substring(0, ontologyXmlNameSpace.IndexOf("#"));
            }

            var ontologyTerms = xDocument.Descendants(owlNamespace + "Class")
                .Where(x => x.Attribute(rdfNamespace + "about") != null)
                .Select(x => new OntologyTerm
                {
                    TermRaw = x.ToString(),
                    TermUri = x.Attribute(rdfNamespace + "about").Value,
                    //TermName = UrlHelper.ExtractTermIdentifierFromUrl(x.Attribute(rdfNamespace + "about").Value),
                    TermName = string.IsNullOrEmpty(x.Descendants(rdfsNamespace + "label")?.FirstOrDefault()?.Value)
                                    ? UrlHelper.ExtractTermIdentifierFromUrl(x.Attribute(rdfNamespace + "about").Value)
                                    : x.Descendants(rdfsNamespace + "label")?.FirstOrDefault()?.Value,
                    ParentTermURI = x.Descendants(rdfsNamespace + "subClassOf")?.FirstOrDefault(y => !y.HasElements)?.Attribute(rdfNamespace + "resource").Value,
                    Ontology = ontology
                })
                .ToList();

            ontologyTerms = ontologyTerms.Where(x => !string.IsNullOrEmpty(x.TermName)).ToList();

            ArrangeTerm(ontologyTerms);

            ontology.OntologyTerms = ontologyTerms;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="ontology"></param>
        /// <returns></returns>
        public int Remove(Ontology ontology)
        {
            _ontologyRepository.Remove(ontology.Id);

            return _ontologyRepository.SaveChanges();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="ontology"></param>
        /// <returns></returns>

        public int Update(Ontology ontology)
        {
            _ontologyRepository.Update(ontology);

            return _ontologyRepository.SaveChanges();
        }

        #endregion IOntologyService public methods

        #region Private methods

        /// <summary>
        ///
        /// </summary>
        /// <param name="terms"></param>
        private void ArrangeTerm(List<OntologyTerm> terms)
        {
            var copiedList = new List<OntologyTerm>(terms);
            OntologyTerm originalParentTerm;
            OntologyTerm originalTerm;

            foreach (var copy in copiedList)
            {
                if (!string.IsNullOrEmpty(copy.ParentTermURI))
                {
                    originalParentTerm = terms.FirstOrDefault(x => x.TermUri == copy.ParentTermURI);

                    if (originalParentTerm != null)
                    {
                        if (originalParentTerm.ChildrenOntologyTerms == null)
                        {
                            originalParentTerm.ChildrenOntologyTerms = new List<OntologyTerm>();
                        }

                        originalParentTerm.ChildrenOntologyTerms.Add(copy);

                        originalTerm = terms.FirstOrDefault(x => x.TermUri == copy.TermUri);
                        originalTerm.MarkItemToBeRemoved();
                    }
                }
            }

            RemoveItemsMarkedToBeRemoved(terms);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="ontologyId"></param>
        /// <param name="data"></param>
        /// <param name="color"></param>
        /// <returns></returns>
        private string CreateHtmlListOfTerms(int ontologyId, IEnumerable<OntologyTerm> data, string color)
        {
            var htmlBuilder = new StringBuilder();

            string termDisplayName;

            foreach (var term in data)
            {
                termDisplayName = term.TermName;

                //if (term.TermName.Length > 21)
                //{
                //    termDisplayName = $"{term.TermName.Substring(0, 20)}...";
                //}

                htmlBuilder.Append($"<li class='treeview ontologyElementContextMenu' ontology-id='{ontologyId}' term-id='{term.Id }'>".Trim());
                htmlBuilder.Append($"   <a href='#'>".Trim());
                htmlBuilder.Append($"       <i class='fa fa-circle-o' style='color: {color};'></i>".Trim());
                htmlBuilder.Append($"       <span data-toggle='tooltip' data-placement='top' title='{term.TermName}'>{termDisplayName}</span>".Trim());

                var hasChildrenTerms = term.ChildrenOntologyTerms != null && term.ChildrenOntologyTerms.Any();

                if (hasChildrenTerms)
                {
                    htmlBuilder.Append("   <span class='pull-right-container'>".Trim());
                    htmlBuilder.Append("       <i class='fa fa-angle-right pull-left'></i>".Trim());
                    htmlBuilder.Append("   </span>".Trim());
                }

                htmlBuilder.Append("   </a>".Trim());

                if (hasChildrenTerms)
                {
                    htmlBuilder.Append("   <ul class='treeview-menu'>".Trim());
                    htmlBuilder.Append(CreateHtmlListOfTerms(ontologyId, term.ChildrenOntologyTerms, color));
                    htmlBuilder.Append("   </ul>".Trim());
                }

                htmlBuilder.Append("</li>");
            };

            return htmlBuilder.ToString();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="ontologyTerms"></param>
        /// <param name="idTerm"></param>
        /// <returns></returns>
        private OntologyTerm FindTerm(IEnumerable<OntologyTerm> ontologyTerms, int idTerm)
        {
            OntologyTerm termFound = null;

            termFound = ontologyTerms.FirstOrDefault(x => x.Id == idTerm);

            if (termFound != null)
            {
                return termFound;
            }
            else
            {
                foreach (var term in ontologyTerms)
                {
                    if (term.ChildrenOntologyTerms != null && term.ChildrenOntologyTerms.Any())
                    {
                        termFound = FindTerm(term.ChildrenOntologyTerms, idTerm);

                        if (termFound != null)
                        {
                            break;
                        }
                    }
                }

                return termFound;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="terms"></param>
        private void RemoveItemsMarkedToBeRemoved(List<OntologyTerm> terms)
        {
            var termsMarkedToBeRemoved = terms.Where(x => x.IsItemMarkedToBeRemoved());
            foreach (var item in termsMarkedToBeRemoved.Reverse())
            {
                terms.Remove(item);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="rootTerm"></param>
        /// <param name="targetTerm"></param>
        /// <returns></returns>
        private IEnumerable<OntologyTerm> TracePath(OntologyTerm rootTerm, OntologyTerm targetTerm)
        {
            var path = new List<OntologyTerm>
            {
                rootTerm
            };

            var targetTermFound = rootTerm.ChildrenOntologyTerms.FirstOrDefault(x => x.Id == targetTerm.Id);

            if (targetTermFound != null)
            {
                path.Add(targetTermFound);

                return path;
            }
            else
            {
                foreach (var item in rootTerm.ChildrenOntologyTerms)
                {
                    //if (item.Id == targetTerm.Id)
                    //{
                    //}

                    var r = TracePath(item, targetTerm);
                    if (r != null)
                    {
                        path.AddRange(r);

                        return path;
                    }
                }
            }

            return null;
        }

        #endregion Private methods
    }
}