using MaternityHospitalBrui.Exceptions;
using MaternityHospitalBrui.Features.UserProfile.Commands;
using MaternityHospitalBrui.Filters;
using MaternityHospitalBrui.Repository;
using MaternityHospitalBrui.States;
using MaternityHospitalBrui.States.Interfaces;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace MaternityHospitalBrui.Features
{
    public interface IPatientFeature
    {
        Task<IPatientState> Create(PatientCommand model);
        Task<IReadOnlyCollection<PatientCommand>> GetAll();
        Task<PatientCommand> GetById(int id);
        Task<PatientCommand> Update(PatientCommand model, int id);
        Task Delete(int id);
        Task<IReadOnlyCollection<PatientState>> Search(string dateFilter);
    }
    public class PatientFeature : IPatientFeature
    {
        private readonly IAppDbContext _dbContext;

        public PatientFeature(IAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IPatientState> Create(PatientCommand model)
        {
            await _dbContext.BeginTransaction();
            try
            {
                var nameEntity = await _dbContext.Name.Create(new NameState
                {
                    Use = model.Name.Use,
                    Family = model.Name.Family,
                    Given = model.Name.Given
                });
                await _dbContext.SaveChanges();
                var entities = _dbContext.Name.OutCreateId();
                if (entities.Length > 0)
                {
                    nameEntity = entities.Last();
                }
                var patientEntity = await _dbContext.Patient.Create(new PatientState
                {
                    NameId = nameEntity.Id,
                    Gender = model.Gender,
                    BirthDate = model.BirthDate,
                    Active = model.Active
                });
                await _dbContext.SaveChanges();
                await _dbContext.CommitTransaction();
                return patientEntity;
            }
            catch (Exception ex)
            {
                await _dbContext.RollBack();
                throw new UnexpectedError("Не удалось добавить...", ex);
            }
        }

        public async Task<IReadOnlyCollection<PatientCommand>> GetAll()
        {
            List<PatientCommand> patients = new List<PatientCommand>();
            var allPatients = await _dbContext.Patient.GetAll();
            foreach (var item in allPatients)
            {
                var name = await _dbContext.Name.Get(item.NameId);
                patients.Add(GetPatientCommand(item, name));
            }
            return patients;
        }

        public async Task<PatientCommand> GetById(int id)
        {
            var patient = await _dbContext.Patient.Get(id);
            var name = await _dbContext.Name.Get(patient.NameId);
            return GetPatientCommand(patient, name);
        }

        private PatientCommand GetPatientCommand(PatientState patient, NameState name)
        {
            return new PatientCommand
            {
                Id = patient.Id,
                Name = new Name
                {
                    Id = name.Id,
                    Use = name.Use,
                    Family = name.Family,
                    Given = name.Given,
                },
                BirthDate = patient.BirthDate,
                Active = patient.Active,
                Gender = patient.Gender
            };
        }

        public async Task<PatientCommand> Update(PatientCommand model, int id)
        {
            var oldPatient = await _dbContext.Patient.Get(id);
            var oldName = await _dbContext.Name.Get(oldPatient.NameId);

            oldPatient.Gender = model.Gender;
            oldPatient.BirthDate = model.BirthDate;
            oldPatient.Active = model.Active;

            oldName.Use = model.Name.Use;
            oldName.Family = model.Name.Family;
            oldName.Given = model.Name.Given;
            await _dbContext.BeginTransaction();
            try
            {
                var patient = await _dbContext.Patient.Update(id, oldPatient);
                await _dbContext.SaveChanges();
                var name = await _dbContext.Name.Update(oldPatient.NameId, oldName);
                await _dbContext.SaveChanges();
                await _dbContext.CommitTransaction();
                return GetPatientCommand(patient, name);
            }
            catch(Exception ex)
            {
                await _dbContext.RollBack();
                throw new UnexpectedError("Не удалось обновить...", ex);
            }
        }

        public async Task Delete(int id)
        {
            try
            {
                var patient = await _dbContext.Patient.Get(id);
                await _dbContext.Name.Delete(patient.NameId);
                await _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new UnexpectedError("Не удалось удалить...", ex);
            }
        }

        public async Task<IReadOnlyCollection<PatientState>> Search(string dateFilter)
        {          
            var dateOnly = dateFilter.Substring(2);
            var filter = dateFilter.Substring(0, 2);
            var date = DateTime.Parse(dateOnly);
            var sort = await _dbContext.Patient.GetAll(filter: new PatientFilter
            {
                BirthDate = date,
                Param = filter
            });
            return sort.ToList(); 
        }
    }
}