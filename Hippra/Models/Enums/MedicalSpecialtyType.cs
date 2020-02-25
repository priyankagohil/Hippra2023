using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Hippra.Models.Enums
{
    public enum MedicalSpecialtyType
    {
        Anesthesiology,
        [Display(Name = "Colon and Rectal Surgery")]
        ColonAndRectalSurgery,
        Dermatology,
        [Display(Name = "Emergency Medicine")]
        EmergencyMedicine,
        [Display(Name = "Family Medicine")]
        FamilyMedicine,
        [Display(Name = "Internal Medicine")]
        InternalMedicine,
        [Display(Name = "Medical Genetics")]
        MedicalGenetics,
        Neurology,
        Neurosurgery,
        [Display(Name = "Nuclear Medicine")]
        NuclearMedicine,
        [Display(Name = "Obstetrics and Gynecology")]
        ObstetricsAndGynecology,
        Ophthalmology,
        OrthopedicSurgery,
        Otolaryngology,
        [Display(Name = "Anatomic Pathology and Clinical Pathology")]
        AnatomicPathologyAndClinicalPathology,
        Pediatrics,
        [Display(Name = "Physical Medicine and Rehibilitation")]
        PhysicalMedicineAndRehibilitation,
        [Display(Name = "Plastic Surgery")]
        PlasticSurgery,
        [Display(Name = "Public Health and General Preventive")]
        PublicHealthAndGeneralPreventive,
        Psychiatry,
        Radiology,
        [Display(Name = "Hospice and Palliative Medicine")]
        HospiceAndPalliativeMedicine,
        [Display(Name = "Medical Nuclear Physics")]
        MedicalNuclearPhysics,
        Surgery,
        [Display(Name = "Vascular Surgery")]
        VascularSurgery,
        [Display(Name = "Thoracic Surgery")]
        ThoracicSurgery,
        Urology




    }
}
