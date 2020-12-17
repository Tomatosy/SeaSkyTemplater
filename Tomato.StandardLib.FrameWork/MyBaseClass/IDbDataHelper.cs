// ===============================================================================
// This file is based on the Microsoft Data Access Application Block for .NET
// For more information please go to 
// http://msdn.microsoft.com/library/en-us/dnbda/html/daab-rm.asp
// ===============================================================================

using System;
using System.Collections.ObjectModel;
using System.Data;

namespace Tomato.StandardLib.MyBaseClass
{
    /// <summary>
    /// ���ݿ�������󣨽ӿڣ�
    /// ��������ݿ���������̳д˽ӿ�
    /// </summary>
    public interface IDbDataHelper
    {
        /// <summary>
        /// ��ȡSQL����ǰ׺��Ĭ��Ϊ@
        /// </summary>
        string ParameterPrefix { get; }

        /// <summary>
        /// ��ȡSQL�ֶ�ǰ׺��Ĭ��Ϊ[
        /// </summary>
        string Prefix { get; }

        /// <summary>
        /// ��ȡSQL�ֶκ�׺��Ĭ��Ϊ]
        /// </summary>
        string Suffix { get; }

        /// <summary>
        /// ����Model�෵�ض�Ӧ�����ݿ���ṹ��Ϣ
        /// </summary>
        /// <param name="t">Model�������</param>
        /// <returns>��Ӧ�����ݿ���ṹ��Ϣ</returns>
        TableInfo GetTableInformation(Type t);

        /// <summary>
        /// ��ȡָ�����ݱ����µ��Զ�����
        /// </summary>
        /// <param name="tableName">���ݱ���</param>
        /// <returns>�Զ�����</returns>
        int ExecuteForIdentity(string tableName);

        /// <summary>
        /// ִ��SQL�����Ӱ���¼����
        /// </summary>
        /// <param name="cmdType">ִ����������</param>
        /// <param name="cmdText">ִ��SQL����</param>
        /// <param name="cmdParms">ִ��SQL����,���鷽ʽ</param>
        /// <returns>ִ�к�Ӱ���¼����</returns>
        int ExecuteNonQuery(CommandType cmdType, string cmdText, params IDataParameter[] cmdParms);

        /// <summary>
        /// ִ��SQL�����Ӱ���¼����
        /// </summary>
        /// <param name="cmdType">ִ����������</param>
        /// <param name="cmdText">ִ��SQL����</param>
        /// <param name="cmdParms">ִ��SQL����,���Ϸ�ʽ</param>
        /// <returns>ִ�к�Ӱ���¼����</returns>
        int ExecuteNonQuery(CommandType cmdType, string cmdText, Collection<IDataParameter> cmdParms);

        /// <summary>
        /// ִ��SQL�����Ӱ���¼����
        /// CommandType����ΪCommandType.Text
        /// </summary>
        /// <param name="cmdText">ִ��SQL����</param>
        /// <param name="cmdParms">ִ��SQL����,���鷽ʽ</param>
        /// <returns>ִ�к�Ӱ���¼����</returns>
        int ExecuteNonQuery(string cmdText, params IDataParameter[] cmdParms);

        /// <summary>
        /// ִ��SQL�����Ӱ���¼����
        /// CommandType����ΪCommandType.Text
        /// </summary>
        /// <param name="cmdText">ִ��SQL����</param>
        /// <param name="cmdParms">ִ��SQL����,���Ϸ�ʽ</param>
        /// <returns>ִ�к�Ӱ���¼����</returns>
        int ExecuteNonQuery(string cmdText, Collection<IDataParameter> cmdParms);

        /// <summary>
        /// ִ��SQL��������ݱ�
        /// </summary>
        /// <param name="cmdType">ִ����������</param>
        /// <param name="cmdText">ִ��SQL����</param>
        /// <param name="cmdParms">ִ��SQL����,���鷽ʽ</param>
        /// <returns>ִ�к󷵻ص����ݱ�</returns>
        DataTable FillDataTable(CommandType cmdType, string cmdText, params IDataParameter[] cmdParms);

        /// <summary>
        /// ִ��SQL��������ݱ�
        /// </summary>
        /// <param name="cmdType">ִ����������</param>
        /// <param name="cmdText">ִ��SQL����</param>
        /// <param name="cmdParms">ִ��SQL����,���Ϸ�ʽ</param>
        /// <returns>ִ�к󷵻ص����ݱ�</returns>
        DataTable FillDataTable(CommandType cmdType, string cmdText, Collection<IDataParameter> cmdParms);

        /// <summary>
        /// ִ��SQL��������ݱ�
        /// CommandType����ΪCommandType.Text
        /// </summary>
        /// <param name="cmdText">ִ��SQL����</param>
        /// <param name="cmdParms">ִ��SQL����,���鷽ʽ</param>
        /// <returns>ִ�к󷵻ص����ݱ�</returns>
        DataTable FillDataTable(string cmdText, params IDataParameter[] cmdParms);

        /// <summary>
        /// ִ��SQL��������ݱ�
        /// CommandType����ΪCommandType.Text
        /// </summary>
        /// <param name="cmdText">ִ��SQL����</param>
        /// <param name="cmdParms">ִ��SQL����,���Ϸ�ʽ</param>
        /// <returns>ִ�к󷵻ص����ݱ�</returns>
        DataTable FillDataTable(string cmdText, Collection<IDataParameter> cmdParms);

        /// <summary>
        /// ִ��SQL��������ݼ�
        /// </summary>
        /// <param name="cmdType">ִ����������</param>
        /// <param name="cmdText">ִ��SQL����</param>
        /// <param name="cmdParms">ִ��SQL����,���鷽ʽ</param>
        /// <returns>ִ�к󷵻ص����ݼ�</returns>
        DataSet FillDataSet(CommandType cmdType, string cmdText, params IDataParameter[] cmdParms);

        /// <summary>
        /// ִ��SQL��������ݼ�
        /// </summary>
        /// <param name="cmdType">ִ����������</param>
        /// <param name="cmdText">ִ��SQL����</param>
        /// <param name="cmdParms">ִ��SQL����,���Ϸ�ʽ</param>
        /// <returns>ִ�к󷵻ص����ݼ�</returns>
        DataSet FillDataSet(CommandType cmdType, string cmdText, Collection<IDataParameter> cmdParms);

        /// <summary>
        /// ִ��SQL��������ݼ�
        /// CommandType����ΪCommandType.Text
        /// </summary>
        /// <param name="cmdText">ִ��SQL����</param>
        /// <param name="cmdParms">ִ��SQL����,���鷽ʽ</param>
        /// <returns>ִ�к󷵻ص����ݼ�</returns>
        DataSet FillDataSet(string cmdText, params IDataParameter[] cmdParms);

        /// <summary>
        /// ִ��SQL��������ݼ�
        /// CommandType����ΪCommandType.Text
        /// </summary>
        /// <param name="cmdText">ִ��SQL����</param>
        /// <param name="cmdParms">ִ��SQL����,���Ϸ�ʽ</param>
        /// <returns>ִ�к󷵻ص����ݼ�</returns>
        DataSet FillDataSet(string cmdText, Collection<IDataParameter> cmdParms);

        /// <summary>
        /// ִ��SQL�����DataReader
        /// </summary>
        /// <param name="cmdType">ִ����������</param>
        /// <param name="cmdText">ִ��SQL����</param>
        /// <param name="cmdParms">ִ��SQL����,���鷽ʽ</param>
        /// <returns>ִ�к󷵻ص�DataReader</returns>
        IDataReader ExecuteReader(CommandType cmdType, string cmdText, params IDataParameter[] cmdParms);

        /// <summary>
        /// ִ��SQL�����DataReader
        /// </summary>
        /// <param name="cmdType">ִ����������</param>
        /// <param name="cmdText">ִ��SQL����</param>
        /// <param name="cmdParms">ִ��SQL����,���Ϸ�ʽ</param>
        /// <returns>ִ�к󷵻ص�DataReader</returns>
        IDataReader ExecuteReader(CommandType cmdType, string cmdText, Collection<IDataParameter> cmdParms);

        /// <summary>
        /// ִ��SQL�����DataReader
        /// CommandType����ΪCommandType.Text
        /// </summary>
        /// <param name="cmdText">ִ��SQL����</param>
        /// <param name="cmdParms">ִ��SQL����,���鷽ʽ</param>
        /// <returns>ִ�к󷵻ص�DataReader</returns>
        IDataReader ExecuteReader(string cmdText, params IDataParameter[] cmdParms);

        /// <summary>
        /// ִ��SQL�����DataReader
        /// CommandType����ΪCommandType.Text
        /// </summary>
        /// <param name="cmdText">ִ��SQL����</param>
        /// <param name="cmdParms">ִ��SQL����,���Ϸ�ʽ</param>
        /// <returns>ִ�к󷵻ص�DataReader</returns>
        IDataReader ExecuteReader(string cmdText, Collection<IDataParameter> cmdParms);

        /// <summary>
        /// ִ��SQL�����DataReader,(�����к�������Ϣ)
        /// </summary>
        /// <param name="cmdType">ִ����������</param>
        /// <param name="cmdText">ִ��SQL����</param>
        /// <param name="cmdParms">ִ��SQL����,���鷽ʽ</param>
        /// <returns>ִ�к󷵻ص�DataReader(�к�������Ϣ)</returns>
        IDataReader ExecuteReaderForKeyInfo(CommandType cmdType, string cmdText, params IDataParameter[] cmdParms);

        /// <summary>
        /// ִ��SQL�����DataReader,(�����к�������Ϣ)
        /// </summary>
        /// <param name="cmdType">ִ����������</param>
        /// <param name="cmdText">ִ��SQL����</param>
        /// <param name="cmdParms">ִ��SQL����,���Ϸ�ʽ</param>
        /// <returns>ִ�к󷵻ص�DataReader(�к�������Ϣ)</returns>
        IDataReader ExecuteReaderForKeyInfo(CommandType cmdType, string cmdText, Collection<IDataParameter> cmdParms);

        /// <summary>
        /// ִ��SQL�����DataReader,(�����к�������Ϣ)
        /// CommandType����ΪCommandType.Text
        /// </summary>
        /// <param name="cmdText">ִ��SQL����</param>
        /// <param name="cmdParms">ִ��SQL����,���鷽ʽ</param>
        /// <returns>ִ�к󷵻ص�DataReader(�к�������Ϣ)</returns>
        IDataReader ExecuteReaderForKeyInfo(string cmdText, params IDataParameter[] cmdParms);

        /// <summary>
        /// ִ��SQL�����DataReader,(�����к�������Ϣ)
        /// CommandType����ΪCommandType.Text
        /// </summary>
        /// <param name="cmdText">ִ��SQL����</param>
        /// <param name="cmdParms">ִ��SQL����,���Ϸ�ʽ</param>
        /// <returns>ִ�к󷵻ص�DataReader(�к�������Ϣ)</returns>
        IDataReader ExecuteReaderForKeyInfo(string cmdText, Collection<IDataParameter> cmdParms);

        /// <summary>
        /// ִ��SQL������������ж��󣬼���Ϊ��ʱ����null
        /// </summary>
        /// <param name="cmdType">ִ����������</param>
        /// <param name="cmdText">ִ��SQL����</param>
        /// <param name="cmdParms">ִ��SQL����,���鷽ʽ</param>
        /// <returns>ִ�к󷵻ص��������ж���</returns>
        object ExecuteScalar(CommandType cmdType, string cmdText, params IDataParameter[] cmdParms);

        /// <summary>
        /// ִ��SQL������������ж��󣬼���Ϊ��ʱ����null
        /// </summary>
        /// <param name="cmdType">ִ����������</param>
        /// <param name="cmdText">ִ��SQL����</param>
        /// <param name="cmdParms">ִ��SQL����,���Ϸ�ʽ</param>
        /// <returns>ִ�к󷵻ص��������ж���</returns>
        object ExecuteScalar(CommandType cmdType, string cmdText, Collection<IDataParameter> cmdParms);

        /// <summary>
        /// ִ��SQL������������ж��󣬼���Ϊ��ʱ����null
        /// CommandType����ΪCommandType.Text
        /// </summary>
        /// <param name="cmdText">ִ��SQL����</param>
        /// <param name="cmdParms">ִ��SQL����,���鷽ʽ</param>
        /// <returns>ִ�к󷵻ص��������ж���</returns>
        object ExecuteScalar(string cmdText, params IDataParameter[] cmdParms);

        /// <summary>
        /// ִ��SQL������������ж��󣬼���Ϊ��ʱ����null
        /// CommandType����ΪCommandType.Text
        /// </summary>
        /// <param name="cmdText">ִ��SQL����</param>
        /// <param name="cmdParms">ִ��SQL����,���Ϸ�ʽ</param>
        /// <returns>ִ�к󷵻ص��������ж���</returns>
        object ExecuteScalar(string cmdText, Collection<IDataParameter> cmdParms);

        /// <summary>
        /// ����SQL��������
        /// </summary>
        /// <param name="parameterName">��������</param>
        /// <param name="p_dbType">���ݿ���������</param>
        /// <param name="srcColumn">���ݿ�����</param>
        /// <returns>SQL��������</returns>
        IDataParameter Parameter(string parameterName, object p_dbType, string srcColumn);

        /// <summary>
        /// ����Model���е��ֶ����Ի�ȡ��Ӧ�����ݿ���������
        /// </summary>
        /// <param name="f">Model���е��ֶ�����</param>
        /// <returns>��Ӧ�����ݿ���������</returns>
        object DbType(MyAttribute.DBFieldInfoAttribute f);
    }
}