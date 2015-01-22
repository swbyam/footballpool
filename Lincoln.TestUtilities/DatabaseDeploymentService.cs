
//-----------------------------------------------------------------------
// <copyright file="DatabaseDeploymentService.cs" company="Lincoln">
//     Copyright (c) Lincoln. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Lincoln.TestUtilities
{
    using System;

    using Microsoft.SqlServer.Dac;
    
    /// <summary>
    /// Service class that is responsible for database deployment-related operations including exporting a database as a bacpac and importing the corresponding bacpac to a database server.
    /// </summary>
    public class DatabaseDeploymentService
    {
        #region Member Variables

        /// <summary>
        /// DacServices instance that is used to perform operations related to bacpacs.
        /// </summary>
        private DacServices dacService;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseDeploymentService"/> class.
        /// </summary>
        /// <param name="targetConnectionString">Connection string to the database upon which the database deployment service is to operate.</param>
        public DatabaseDeploymentService(string targetConnectionString)
        {
            if (string.IsNullOrWhiteSpace(targetConnectionString))
            {
                throw new ArgumentException("targetConnectionString cannot be null or empty string.", "targetConnectionString");
            }

            this.dacService = new DacServices(targetConnectionString);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Exports a database with name <paramref name="exportDatabase"/> as a bacpac to the folder path specified by <paramref name="bacPacExportPath"/>.
        /// </summary>
        /// <param name="exportDatabase">Name of the database to export.</param>
        /// <param name="bacPacExportPath">Path to which the corresponding bacpac is to be exported.</param>
        public void ExportBacPac(string exportDatabase, string bacPacExportPath)
        {
            if (string.IsNullOrWhiteSpace(exportDatabase))
            {
                throw new ArgumentException("exportDatabase cannot be null or empty string.", "exportDatabase");
            }

            if (string.IsNullOrWhiteSpace(bacPacExportPath))
            {
                throw new ArgumentException("bacPacExportPath cannot be null or empty string.", "bacPacExportPath");
            }

            try
            {
                this.dacService.ExportBacpac(bacPacExportPath, exportDatabase);
            }
            catch (DacServicesException dsExcp)
            {
                ////TODO: Handle exception!
                throw;
            }
        }

        /// <summary>
        /// Imports a bacpac contained at the file path referenced by <paramref name="bacPacImportPath"/> to a database with the name <paramref name="importDatabase"/>.
        /// </summary>
        /// <param name="importDatabase">Database to which the bacpac is to be imported.</param>
        /// <param name="bacPacImportPath">Path where the bacpac to be imported is located.</param>
        public void ImportBacPac(string importDatabase, string bacPacImportPath)
        {
            if (string.IsNullOrWhiteSpace(importDatabase))
            {
                throw new ArgumentException("importDatabase cannot be null or empty string.", "importDatabase");
            }

            if (string.IsNullOrWhiteSpace(bacPacImportPath))
            {
                throw new ArgumentException("bacPacImportPath cannot be null or empty string.", "bacPacImportPath");
            }

            try
            {
                using (BacPackage bacpac = BacPackage.Load(bacPacImportPath))
                {
                    this.dacService.ImportBacpac(bacpac, importDatabase);
                }
            }
            catch (DacServicesException dsExcp)
            {
                ////TODO: Handle exception!
                throw;
            }
        }

        #endregion
    }
}
