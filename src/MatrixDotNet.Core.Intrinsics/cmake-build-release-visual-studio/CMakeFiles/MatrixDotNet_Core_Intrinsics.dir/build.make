# CMAKE generated file: DO NOT EDIT!
# Generated by "NMake Makefiles" Generator, CMake Version 3.16

# Delete rule output on recipe failure.
.DELETE_ON_ERROR:


#=============================================================================
# Special targets provided by cmake.

# Disable implicit rules so canonical targets will work.
.SUFFIXES:


.SUFFIXES: .hpux_make_needs_suffix_list


# Suppress display of executed commands.
$(VERBOSE).SILENT:


# A target that is always out of date.
cmake_force:

.PHONY : cmake_force

#=============================================================================
# Set environment variables for the build.

!IF "$(OS)" == "Windows_NT"
NULL=
!ELSE
NULL=nul
!ENDIF
SHELL = cmd.exe

# The CMake executable.
CMAKE_COMMAND = "D:\CLion 2020.1.2\bin\cmake\win\bin\cmake.exe"

# The command to remove a file.
RM = "D:\CLion 2020.1.2\bin\cmake\win\bin\cmake.exe" -E remove -f

# Escaping for special characters.
EQUALS = =

# The top-level source directory on which CMake was run.
CMAKE_SOURCE_DIR = D:\RiderProjects\MatrixDotNet\src\MatrixDotNet.Core.Intrinsics

# The top-level build directory on which CMake was run.
CMAKE_BINARY_DIR = D:\RiderProjects\MatrixDotNet\src\MatrixDotNet.Core.Intrinsics\cmake-build-release-visual-studio

# Include any dependencies generated for this target.
include CMakeFiles\MatrixDotNet_Core_Intrinsics.dir\depend.make

# Include the progress variables for this target.
include CMakeFiles\MatrixDotNet_Core_Intrinsics.dir\progress.make

# Include the compile flags for this target's objects.
include CMakeFiles\MatrixDotNet_Core_Intrinsics.dir\flags.make

CMakeFiles\MatrixDotNet_Core_Intrinsics.dir\src\library.cpp.obj: CMakeFiles\MatrixDotNet_Core_Intrinsics.dir\flags.make
CMakeFiles\MatrixDotNet_Core_Intrinsics.dir\src\library.cpp.obj: ..\src\library.cpp
	@$(CMAKE_COMMAND) -E cmake_echo_color --switch=$(COLOR) --green --progress-dir=D:\RiderProjects\MatrixDotNet\src\MatrixDotNet.Core.Intrinsics\cmake-build-release-visual-studio\CMakeFiles --progress-num=$(CMAKE_PROGRESS_1) "Building CXX object CMakeFiles/MatrixDotNet_Core_Intrinsics.dir/src/library.cpp.obj"
	"C:\Program Files (x86)\MICROS~3\2019\COMMUN~1\VC\Tools\MSVC\1427~1.291\bin\Hostx64\x64\cl.exe" @<<
 /nologo /TP $(CXX_DEFINES) $(CXX_INCLUDES) $(CXX_FLAGS) /FoCMakeFiles\MatrixDotNet_Core_Intrinsics.dir\src\library.cpp.obj /FdCMakeFiles\MatrixDotNet_Core_Intrinsics.dir\ /FS -c D:\RiderProjects\MatrixDotNet\src\MatrixDotNet.Core.Intrinsics\src\library.cpp
<<

CMakeFiles\MatrixDotNet_Core_Intrinsics.dir\src\library.cpp.i: cmake_force
	@$(CMAKE_COMMAND) -E cmake_echo_color --switch=$(COLOR) --green "Preprocessing CXX source to CMakeFiles/MatrixDotNet_Core_Intrinsics.dir/src/library.cpp.i"
	"C:\Program Files (x86)\MICROS~3\2019\COMMUN~1\VC\Tools\MSVC\1427~1.291\bin\Hostx64\x64\cl.exe" > CMakeFiles\MatrixDotNet_Core_Intrinsics.dir\src\library.cpp.i @<<
 /nologo /TP $(CXX_DEFINES) $(CXX_INCLUDES) $(CXX_FLAGS) -E D:\RiderProjects\MatrixDotNet\src\MatrixDotNet.Core.Intrinsics\src\library.cpp
<<

CMakeFiles\MatrixDotNet_Core_Intrinsics.dir\src\library.cpp.s: cmake_force
	@$(CMAKE_COMMAND) -E cmake_echo_color --switch=$(COLOR) --green "Compiling CXX source to assembly CMakeFiles/MatrixDotNet_Core_Intrinsics.dir/src/library.cpp.s"
	"C:\Program Files (x86)\MICROS~3\2019\COMMUN~1\VC\Tools\MSVC\1427~1.291\bin\Hostx64\x64\cl.exe" @<<
 /nologo /TP $(CXX_DEFINES) $(CXX_INCLUDES) $(CXX_FLAGS) /FoNUL /FAs /FaCMakeFiles\MatrixDotNet_Core_Intrinsics.dir\src\library.cpp.s /c D:\RiderProjects\MatrixDotNet\src\MatrixDotNet.Core.Intrinsics\src\library.cpp
<<

CMakeFiles\MatrixDotNet_Core_Intrinsics.dir\headers\StringBuilder.cpp.obj: CMakeFiles\MatrixDotNet_Core_Intrinsics.dir\flags.make
CMakeFiles\MatrixDotNet_Core_Intrinsics.dir\headers\StringBuilder.cpp.obj: ..\headers\StringBuilder.cpp
	@$(CMAKE_COMMAND) -E cmake_echo_color --switch=$(COLOR) --green --progress-dir=D:\RiderProjects\MatrixDotNet\src\MatrixDotNet.Core.Intrinsics\cmake-build-release-visual-studio\CMakeFiles --progress-num=$(CMAKE_PROGRESS_2) "Building CXX object CMakeFiles/MatrixDotNet_Core_Intrinsics.dir/headers/StringBuilder.cpp.obj"
	"C:\Program Files (x86)\MICROS~3\2019\COMMUN~1\VC\Tools\MSVC\1427~1.291\bin\Hostx64\x64\cl.exe" @<<
 /nologo /TP $(CXX_DEFINES) $(CXX_INCLUDES) $(CXX_FLAGS) /FoCMakeFiles\MatrixDotNet_Core_Intrinsics.dir\headers\StringBuilder.cpp.obj /FdCMakeFiles\MatrixDotNet_Core_Intrinsics.dir\ /FS -c D:\RiderProjects\MatrixDotNet\src\MatrixDotNet.Core.Intrinsics\headers\StringBuilder.cpp
<<

CMakeFiles\MatrixDotNet_Core_Intrinsics.dir\headers\StringBuilder.cpp.i: cmake_force
	@$(CMAKE_COMMAND) -E cmake_echo_color --switch=$(COLOR) --green "Preprocessing CXX source to CMakeFiles/MatrixDotNet_Core_Intrinsics.dir/headers/StringBuilder.cpp.i"
	"C:\Program Files (x86)\MICROS~3\2019\COMMUN~1\VC\Tools\MSVC\1427~1.291\bin\Hostx64\x64\cl.exe" > CMakeFiles\MatrixDotNet_Core_Intrinsics.dir\headers\StringBuilder.cpp.i @<<
 /nologo /TP $(CXX_DEFINES) $(CXX_INCLUDES) $(CXX_FLAGS) -E D:\RiderProjects\MatrixDotNet\src\MatrixDotNet.Core.Intrinsics\headers\StringBuilder.cpp
<<

CMakeFiles\MatrixDotNet_Core_Intrinsics.dir\headers\StringBuilder.cpp.s: cmake_force
	@$(CMAKE_COMMAND) -E cmake_echo_color --switch=$(COLOR) --green "Compiling CXX source to assembly CMakeFiles/MatrixDotNet_Core_Intrinsics.dir/headers/StringBuilder.cpp.s"
	"C:\Program Files (x86)\MICROS~3\2019\COMMUN~1\VC\Tools\MSVC\1427~1.291\bin\Hostx64\x64\cl.exe" @<<
 /nologo /TP $(CXX_DEFINES) $(CXX_INCLUDES) $(CXX_FLAGS) /FoNUL /FAs /FaCMakeFiles\MatrixDotNet_Core_Intrinsics.dir\headers\StringBuilder.cpp.s /c D:\RiderProjects\MatrixDotNet\src\MatrixDotNet.Core.Intrinsics\headers\StringBuilder.cpp
<<

# Object files for target MatrixDotNet_Core_Intrinsics
MatrixDotNet_Core_Intrinsics_OBJECTS = \
"CMakeFiles\MatrixDotNet_Core_Intrinsics.dir\src\library.cpp.obj" \
"CMakeFiles\MatrixDotNet_Core_Intrinsics.dir\headers\StringBuilder.cpp.obj"

# External object files for target MatrixDotNet_Core_Intrinsics
MatrixDotNet_Core_Intrinsics_EXTERNAL_OBJECTS =

MatrixDotNet_Core_Intrinsics.dll: CMakeFiles\MatrixDotNet_Core_Intrinsics.dir\src\library.cpp.obj
MatrixDotNet_Core_Intrinsics.dll: CMakeFiles\MatrixDotNet_Core_Intrinsics.dir\headers\StringBuilder.cpp.obj
MatrixDotNet_Core_Intrinsics.dll: CMakeFiles\MatrixDotNet_Core_Intrinsics.dir\build.make
MatrixDotNet_Core_Intrinsics.dll: CMakeFiles\MatrixDotNet_Core_Intrinsics.dir\objects1.rsp
	@$(CMAKE_COMMAND) -E cmake_echo_color --switch=$(COLOR) --green --bold --progress-dir=D:\RiderProjects\MatrixDotNet\src\MatrixDotNet.Core.Intrinsics\cmake-build-release-visual-studio\CMakeFiles --progress-num=$(CMAKE_PROGRESS_3) "Linking CXX shared library MatrixDotNet_Core_Intrinsics.dll"
	"D:\CLion 2020.1.2\bin\cmake\win\bin\cmake.exe" -E vs_link_dll --intdir=CMakeFiles\MatrixDotNet_Core_Intrinsics.dir --rc="C:\Program Files (x86)\WINDOW~1\10\bin\100183~1.0\x64\rc.exe" --mt="C:\Program Files (x86)\WINDOW~1\10\bin\100183~1.0\x64\mt.exe" --manifests  -- "C:\Program Files (x86)\MICROS~3\2019\COMMUN~1\VC\Tools\MSVC\1427~1.291\bin\Hostx64\x64\link.exe" /nologo @CMakeFiles\MatrixDotNet_Core_Intrinsics.dir\objects1.rsp @<<
 /out:MatrixDotNet_Core_Intrinsics.dll /implib:MatrixDotNet_Core_Intrinsics.lib /pdb:D:\RiderProjects\MatrixDotNet\src\MatrixDotNet.Core.Intrinsics\cmake-build-release-visual-studio\MatrixDotNet_Core_Intrinsics.pdb /dll /version:0.0 /machine:x64 /INCREMENTAL:NO  kernel32.lib user32.lib gdi32.lib winspool.lib shell32.lib ole32.lib oleaut32.lib uuid.lib comdlg32.lib advapi32.lib  
<<

# Rule to build all files generated by this target.
CMakeFiles\MatrixDotNet_Core_Intrinsics.dir\build: MatrixDotNet_Core_Intrinsics.dll

.PHONY : CMakeFiles\MatrixDotNet_Core_Intrinsics.dir\build

CMakeFiles\MatrixDotNet_Core_Intrinsics.dir\clean:
	$(CMAKE_COMMAND) -P CMakeFiles\MatrixDotNet_Core_Intrinsics.dir\cmake_clean.cmake
.PHONY : CMakeFiles\MatrixDotNet_Core_Intrinsics.dir\clean

CMakeFiles\MatrixDotNet_Core_Intrinsics.dir\depend:
	$(CMAKE_COMMAND) -E cmake_depends "NMake Makefiles" D:\RiderProjects\MatrixDotNet\src\MatrixDotNet.Core.Intrinsics D:\RiderProjects\MatrixDotNet\src\MatrixDotNet.Core.Intrinsics D:\RiderProjects\MatrixDotNet\src\MatrixDotNet.Core.Intrinsics\cmake-build-release-visual-studio D:\RiderProjects\MatrixDotNet\src\MatrixDotNet.Core.Intrinsics\cmake-build-release-visual-studio D:\RiderProjects\MatrixDotNet\src\MatrixDotNet.Core.Intrinsics\cmake-build-release-visual-studio\CMakeFiles\MatrixDotNet_Core_Intrinsics.dir\DependInfo.cmake --color=$(COLOR)
.PHONY : CMakeFiles\MatrixDotNet_Core_Intrinsics.dir\depend
