//-----------------------------------------------------------------------
// <copyright file="IMockNationalLotteryModel.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;

using Foundation.Common;
using Foundation.Interfaces;

using NationalLottery.Interfaces;

using NLModels = NationalLottery.Models;

namespace NationalLottery.UnitTests.Mocks
{
    public interface IMockNationalLotteryModel : INationalLotteryModel
    {
        String Name { get; set; }
        String Code { get; set; }
        String Description { get; set; }
        Image ImagePicture { get; set; }
    }

    [DependencyInjectionTransient]
    public class MockNationalLotteryModel : NLModels.NationalLotteryModel, IMockNationalLotteryModel
    {
        private String _name = default;
        private String _code = default;
        private String _description = default;
        private Image _imagePicture = default;

        public MockNationalLotteryModel(IDateTimeService dateTimeService)
        {
            ValidFrom = dateTimeService.SystemDateTimeNow;
            ValidTo = new DateTime(2100, 12, 31, 23, 59, 59);
        }

        public MockNationalLotteryModel(Int32 id)
        {
            Id = new EntityId(id);
        }

        public MockNationalLotteryModel()
        {
            ValidFrom = DateTime.Now;
            ValidTo = new DateTime(2100, 12, 31, 23, 59, 59);
        }

        [Column("Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Name must be supplied")]
        public String Name
        {
            get => this._name;
            set => this.SetPropertyValue(ref _name, value);
        }

        [Column("Code")]
        public String Code
        {
            get => this._code;
            set => this.SetPropertyValue(ref _code, value);
        }

        [Column("Description")]
        public String Description
        {
            get => this._description;
            set => this.SetPropertyValue(ref _description, value);
        }

        [Column("ImagePicture")]
        public Image ImagePicture
        {
            get => _imagePicture;
            set => this.SetPropertyValue(ref _imagePicture, value);
        }

        public override Object Clone()
        {
            MockNationalLotteryModel retVal = base.Clone() as MockNationalLotteryModel;

            retVal._name = this._name;
            retVal._code = this._code;
            retVal._description = this._description;

            if (this._imagePicture.IsNotNull())
            {
                retVal._imagePicture = this._imagePicture.Clone() as Image;
            }

            return retVal;
        }
    }
}
