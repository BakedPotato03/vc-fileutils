﻿using Microsoft.VisualStudio.VCProjectEngine;
using System.IO;
using System;
using VCFileUtils.Helpers;

namespace VCFileUtils.Model
{
    internal class VCProjectWrapper : ContainerWrapper
    {
        public VCProjectWrapper(VCProject project) : base(project)
        {
        }

        protected VCProject Project 
        {
            get
            {
                return (VCProject)VCProjectItem;
            }
        }

        protected override dynamic _Files 
        {
            get
            {
                return Project.Files;
            }
        }

        protected override dynamic _Filters 
        {
            get
            {
                return Project.Filters;
            }
        }

        public string ProjectFile
        {
            get
            {
                return Project.ProjectFile;
            }
        }

        public override string FilePath
        {
            get
            {
                return Project.ProjectFile;
            }
        }

        protected override VCFilter _AddFilter(string name)
        {
            if (!Project.CanAddFilter(name))
                throw new InvalidOperationException();

            return (VCFilter)Project.AddFilter(name);
        }

        protected override VCFile _AddFile(string path)
        {
            if (!Project.CanAddFile(path))
                throw new InvalidOperationException();

            return (VCFile)Project.AddFile(path);
        }

        public string MakeRelativePath(string absolutePath)
        {
            string root = SettingsManager.GetSettings(this).ProjectRoot + "\\";

            Uri uriRoot = new Uri(root);
            Uri uriPath = new Uri(absolutePath);
            return uriRoot.MakeRelativeUri(uriPath).ToString();
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
