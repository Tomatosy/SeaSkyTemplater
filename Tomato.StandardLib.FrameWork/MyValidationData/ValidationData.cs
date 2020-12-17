/**************************************************************************
*   
*   =================================
*   CLR版本    ：4.0.30319.42000
*   命名空间    ：Tomato.StandardLib.FrameWork.MyValidationData
*   文件名称    ：ValidationData.cs
*   =================================
*   创 建 者    ：Aeck
*   创建日期    ：2019/09/03 10:36:43 
*   邮箱        ：Aeck499@aliyun.com
*   个人主站    ：https://my.oschina.net/u/4123699
*   功能描述    ：
*   使用说明    ：
*   =================================
*   修改者    ：
*   修改日期    ：
*   修改内容    ：
*   =================================
*  
***************************************************************************/
using Tomato.StandardLib.FrameWork.MyModel;
using Tomato.StandardLib.MyModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Tomato.StandardLib.FrameWork
{
    public static class ValidationData
    {

        /// <summary>
        /// 验证模型,发现错误立即返回
        /// </summary>
        public static BaseResultModel<string> ValidationModel<TModel>(TModel model)
        {
            try
            {
                BaseResultModel<List<ValidationErrorMessageModel>> results = ValidationModelAll(model);
                if (!results.IsSuccess)
                {
                    ValidationErrorMessageModel result = results.Data.FirstOrDefault();
                    return new ErrorResultModel<string>(results.ErrorCode, result.ErrorMessage);
                }
                return new SuccessResultModel<string>();
            }
            catch (Exception e)
            {
                return new ErrorResultModel<string>(EnumErrorCode.系统异常, "获取错误列表发生异常");
            }
        }
        /// <summary>
        /// 验证模型,返回全部错误信息,并并拼接成字符串,以逗号分隔
        /// </summary>
        public static BaseResultModel<string> ValidationModelAllString<TModel>(TModel model)
        {
            BaseResultModel<List<ValidationErrorMessageModel>> results = ValidationModelAll(model);
            if (!results.IsSuccess)
            {
                if (results.Data == null || results.Data.Count() == 0)
                {
                    return new SuccessResultModel<string>();
                }
                ValidationErrorMessageModel errorModel = ErrorMessageModelToString(results.Data);
                return new ErrorResultModel<string>(results.ErrorCode, errorModel.ErrorMessage);
            }
            return new SuccessResultModel<string>();
        }
        /// <summary>
        /// 验证模型,返回全部错误信息
        /// </summary>
        public static BaseResultModel<List<ValidationErrorMessageModel>> ValidationModelAll<TModel>(TModel model)
        {
            return TryValidataObject(model);
        }
        /// <summary>
        /// 验证模型,isField=True验证指定字段,isField=Flase验证非指定字段,发现错误立即返回
        /// </summary>
        public static BaseResultModel<string> ValidationField<TModel>(TModel model, params string[] field) { return ValidationField(model, true, field); }
        public static BaseResultModel<string> ValidationField<TModel>(TModel model, bool isField = true, params string[] field)
        {
            BaseResultModel<List<ValidationErrorMessageModel>> results = ValidationFieldAll(model, isField, field);
            if (results.IsSuccess)
            {
                return new SuccessResultModel<string>();
            }
            if (results.Data == null || results.Data.Count() == 0)
            {
                return new SuccessResultModel<string>();
            }
            ValidationErrorMessageModel result = results.Data.FirstOrDefault();
            return new ErrorResultModel<string>(results.ErrorCode, result.ErrorMessage);
        }
        /// <summary>
        /// 验证模型,isField=True验证指定字段,isField=Flase验证非指定字段,返回全部错误信息,并并拼接成字符串,以逗号分隔
        /// </summary>
        public static BaseResultModel<string> ValidationFieldAllString<TModel>(TModel model, params string[] field) { return ValidationFieldAllString(model, true, field); }
        public static BaseResultModel<string> ValidationFieldAllString<TModel>(TModel model, bool isField = true, params string[] field)
        {
            BaseResultModel<List<ValidationErrorMessageModel>> isResults = ValidationFieldAll(model, isField, field);
            if (isResults.IsSuccess)
            {
                return new SuccessResultModel<string>();
            }
            if (isResults.Data == null || isResults.Data.Count() == 0)
            {
                return new SuccessResultModel<string>();
            }
            ValidationErrorMessageModel errorModel = ErrorMessageModelToString(isResults.Data);
            return new ErrorResultModel<string>(isResults.ErrorCode, errorModel.ErrorMessage);
        }
        /// <summary>
        /// 验证模型,isField=True验证指定字段,isField=Flase验证非指定字段,返回全部错误信息
        /// </summary>
        public static BaseResultModel<List<ValidationErrorMessageModel>> ValidationFieldAll<TModel>(TModel model, params string[] field) { return ValidationFieldAll(model, true, field); }
        public static BaseResultModel<List<ValidationErrorMessageModel>> ValidationFieldAll<TModel>(TModel model, bool isField, params string[] field)
        {
            BaseResultModel<List<ValidationErrorMessageModel>> isResults = TryValidataObject(model);
            if (isResults.IsSuccess)
            {
                return new SuccessResultModel<List<ValidationErrorMessageModel>>();
            }
            if (isResults.Data == null || isResults.Data.Count() == 0)
            {
                return isResults;
            }
            List<ValidationErrorMessageModel> results = new List<ValidationErrorMessageModel>();
            try
            {
                foreach (ValidationErrorMessageModel item in isResults.Data)
                {

                    if (field.Where(a => a == item.ErrorName).Count() > 0 && isField)
                    {
                        ValidationErrorMessageModel result = new ValidationErrorMessageModel();
                        result.ErrorName = item.ErrorName;
                        result.ErrorMessage = item.ErrorMessage;
                        results.Add(result);
                    }
                    else if (field.Where(a => a == item.ErrorName).Count() == 0 && !isField)
                    {
                        ValidationErrorMessageModel result = new ValidationErrorMessageModel();
                        result.ErrorName = item.ErrorName;
                        result.ErrorMessage = item.ErrorMessage;
                        results.Add(result);
                    }
                }
                return new ErrorResultModel<List<ValidationErrorMessageModel>>(isResults.ErrorCode, isResults.ErrorMessage) { Data = isResults.Data };
            }
            catch (Exception e)
            {
                return new ErrorResultModel<List<ValidationErrorMessageModel>>(EnumErrorCode.系统异常, "验证指定字段发生异常");
            }
        }


        #region
        /// <summary>
        /// 查询错误集合
        /// </summary>
        private static BaseResultModel<List<ValidationErrorMessageModel>> TryValidataObject<TModel>(TModel model)
        {
            List<ValidationErrorMessageModel> results = new List<ValidationErrorMessageModel>();
            List<ValidationResult> erorrResults = new List<ValidationResult>();
            try
            {
                ValidationContext context = new ValidationContext(model, null, null);
                bool isValid = Validator.TryValidateObject(model, context, erorrResults, true);
                if (!isValid)
                {
                    foreach (ValidationResult items in erorrResults)
                    {
                        foreach (string item in items.MemberNames)
                        {
                            ValidationErrorMessageModel result = new ValidationErrorMessageModel();
                            result.ErrorName = item;
                            result.ErrorMessage = items.ErrorMessage;
                            results.Add(result);
                        }
                    }
                    return new ErrorResultModel<List<ValidationErrorMessageModel>>(EnumErrorCode.参数校验未通过, "参数校验未通过") { Data = results };
                }
                return new SuccessResultModel<List<ValidationErrorMessageModel>>();
            }
            catch (Exception e)
            {
                return new ErrorResultModel<List<ValidationErrorMessageModel>>(EnumErrorCode.系统异常, "传入类型不正确");
            }
        }
        /// <summary>
        /// ErrorMessageModelToString
        /// </summary>
        private static ValidationErrorMessageModel ErrorMessageModelToString(List<ValidationErrorMessageModel> models)
        {
            StringBuilder ErrorNames = new StringBuilder();
            StringBuilder ErrorMessages = new StringBuilder();
            foreach (ValidationErrorMessageModel item in models)
            {
                ErrorNames.Append(item.ErrorName + "，");
                ErrorMessages.Append(item.ErrorMessage + "，");
            }
            ErrorNames = ErrorNames.Remove(ErrorNames.Length - 1, 1);
            ErrorMessages = ErrorMessages.Remove(ErrorMessages.Length - 1, 1);
            return new ValidationErrorMessageModel { ErrorName = ErrorNames.ToString(), ErrorMessage = ErrorMessages.ToString() };
        }
        #endregion
    }
}
