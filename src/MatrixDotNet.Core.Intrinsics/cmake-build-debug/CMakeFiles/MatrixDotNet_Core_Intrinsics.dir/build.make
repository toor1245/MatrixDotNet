# CMAKE generated file: DO NOT EDIT!
# Generated by "Unix Makefiles" Generator, CMake Version 3.17

# Delete rule output on recipe failure.
.DELETE_ON_ERROR:


#=============================================================================
# Special targets provided by cmake.

# Disable implicit rules so canonical targets will work.
.SUFFIXES:


# Disable VCS-based implicit rules.
% : %,v


# Disable VCS-based implicit rules.
% : RCS/%


# Disable VCS-based implicit rules.
% : RCS/%,v


# Disable VCS-based implicit rules.
% : SCCS/s.%


# Disable VCS-based implicit rules.
% : s.%


.SUFFIXES: .hpux_make_needs_suffix_list


# Command-line flag to silence nested $(MAKE).
$(VERBOSE)MAKESILENT = -s

# Suppress display of executed commands.
$(VERBOSE).SILENT:


# A target that is always out of date.
cmake_force:

.PHONY : cmake_force

#=============================================================================
# Set environment variables for the build.

# The shell in which to execute make rules.
SHELL = /bin/sh

# The CMake executable.
CMAKE_COMMAND = /snap/clion/124/bin/cmake/linux/bin/cmake

# The command to remove a file.
RM = /snap/clion/124/bin/cmake/linux/bin/cmake -E rm -f

# Escaping for special characters.
EQUALS = =

# The top-level source directory on which CMake was run.
CMAKE_SOURCE_DIR = /home/nikolay/RiderProjects/MatrixDotNet/src/MatrixDotNet.Core.Intrinsics

# The top-level build directory on which CMake was run.
CMAKE_BINARY_DIR = /home/nikolay/RiderProjects/MatrixDotNet/src/MatrixDotNet.Core.Intrinsics/cmake-build-debug

# Include any dependencies generated for this target.
include CMakeFiles/MatrixDotNet_Core_Intrinsics.dir/depend.make

# Include the progress variables for this target.
include CMakeFiles/MatrixDotNet_Core_Intrinsics.dir/progress.make

# Include the compile flags for this target's objects.
include CMakeFiles/MatrixDotNet_Core_Intrinsics.dir/flags.make

CMakeFiles/MatrixDotNet_Core_Intrinsics.dir/src/library.cpp.o: CMakeFiles/MatrixDotNet_Core_Intrinsics.dir/flags.make
CMakeFiles/MatrixDotNet_Core_Intrinsics.dir/src/library.cpp.o: ../src/library.cpp
	@$(CMAKE_COMMAND) -E cmake_echo_color --switch=$(COLOR) --green --progress-dir=/home/nikolay/RiderProjects/MatrixDotNet/src/MatrixDotNet.Core.Intrinsics/cmake-build-debug/CMakeFiles --progress-num=$(CMAKE_PROGRESS_1) "Building CXX object CMakeFiles/MatrixDotNet_Core_Intrinsics.dir/src/library.cpp.o"
	/usr/bin/c++  $(CXX_DEFINES) $(CXX_INCLUDES) $(CXX_FLAGS) -o CMakeFiles/MatrixDotNet_Core_Intrinsics.dir/src/library.cpp.o -c /home/nikolay/RiderProjects/MatrixDotNet/src/MatrixDotNet.Core.Intrinsics/src/library.cpp

CMakeFiles/MatrixDotNet_Core_Intrinsics.dir/src/library.cpp.i: cmake_force
	@$(CMAKE_COMMAND) -E cmake_echo_color --switch=$(COLOR) --green "Preprocessing CXX source to CMakeFiles/MatrixDotNet_Core_Intrinsics.dir/src/library.cpp.i"
	/usr/bin/c++ $(CXX_DEFINES) $(CXX_INCLUDES) $(CXX_FLAGS) -E /home/nikolay/RiderProjects/MatrixDotNet/src/MatrixDotNet.Core.Intrinsics/src/library.cpp > CMakeFiles/MatrixDotNet_Core_Intrinsics.dir/src/library.cpp.i

CMakeFiles/MatrixDotNet_Core_Intrinsics.dir/src/library.cpp.s: cmake_force
	@$(CMAKE_COMMAND) -E cmake_echo_color --switch=$(COLOR) --green "Compiling CXX source to assembly CMakeFiles/MatrixDotNet_Core_Intrinsics.dir/src/library.cpp.s"
	/usr/bin/c++ $(CXX_DEFINES) $(CXX_INCLUDES) $(CXX_FLAGS) -S /home/nikolay/RiderProjects/MatrixDotNet/src/MatrixDotNet.Core.Intrinsics/src/library.cpp -o CMakeFiles/MatrixDotNet_Core_Intrinsics.dir/src/library.cpp.s

CMakeFiles/MatrixDotNet_Core_Intrinsics.dir/headers/StringBuilder.cpp.o: CMakeFiles/MatrixDotNet_Core_Intrinsics.dir/flags.make
CMakeFiles/MatrixDotNet_Core_Intrinsics.dir/headers/StringBuilder.cpp.o: ../headers/StringBuilder.cpp
	@$(CMAKE_COMMAND) -E cmake_echo_color --switch=$(COLOR) --green --progress-dir=/home/nikolay/RiderProjects/MatrixDotNet/src/MatrixDotNet.Core.Intrinsics/cmake-build-debug/CMakeFiles --progress-num=$(CMAKE_PROGRESS_2) "Building CXX object CMakeFiles/MatrixDotNet_Core_Intrinsics.dir/headers/StringBuilder.cpp.o"
	/usr/bin/c++  $(CXX_DEFINES) $(CXX_INCLUDES) $(CXX_FLAGS) -o CMakeFiles/MatrixDotNet_Core_Intrinsics.dir/headers/StringBuilder.cpp.o -c /home/nikolay/RiderProjects/MatrixDotNet/src/MatrixDotNet.Core.Intrinsics/headers/StringBuilder.cpp

CMakeFiles/MatrixDotNet_Core_Intrinsics.dir/headers/StringBuilder.cpp.i: cmake_force
	@$(CMAKE_COMMAND) -E cmake_echo_color --switch=$(COLOR) --green "Preprocessing CXX source to CMakeFiles/MatrixDotNet_Core_Intrinsics.dir/headers/StringBuilder.cpp.i"
	/usr/bin/c++ $(CXX_DEFINES) $(CXX_INCLUDES) $(CXX_FLAGS) -E /home/nikolay/RiderProjects/MatrixDotNet/src/MatrixDotNet.Core.Intrinsics/headers/StringBuilder.cpp > CMakeFiles/MatrixDotNet_Core_Intrinsics.dir/headers/StringBuilder.cpp.i

CMakeFiles/MatrixDotNet_Core_Intrinsics.dir/headers/StringBuilder.cpp.s: cmake_force
	@$(CMAKE_COMMAND) -E cmake_echo_color --switch=$(COLOR) --green "Compiling CXX source to assembly CMakeFiles/MatrixDotNet_Core_Intrinsics.dir/headers/StringBuilder.cpp.s"
	/usr/bin/c++ $(CXX_DEFINES) $(CXX_INCLUDES) $(CXX_FLAGS) -S /home/nikolay/RiderProjects/MatrixDotNet/src/MatrixDotNet.Core.Intrinsics/headers/StringBuilder.cpp -o CMakeFiles/MatrixDotNet_Core_Intrinsics.dir/headers/StringBuilder.cpp.s

# Object files for target MatrixDotNet_Core_Intrinsics
MatrixDotNet_Core_Intrinsics_OBJECTS = \
"CMakeFiles/MatrixDotNet_Core_Intrinsics.dir/src/library.cpp.o" \
"CMakeFiles/MatrixDotNet_Core_Intrinsics.dir/headers/StringBuilder.cpp.o"

# External object files for target MatrixDotNet_Core_Intrinsics
MatrixDotNet_Core_Intrinsics_EXTERNAL_OBJECTS =

libMatrixDotNet_Core_Intrinsics.so: CMakeFiles/MatrixDotNet_Core_Intrinsics.dir/src/library.cpp.o
libMatrixDotNet_Core_Intrinsics.so: CMakeFiles/MatrixDotNet_Core_Intrinsics.dir/headers/StringBuilder.cpp.o
libMatrixDotNet_Core_Intrinsics.so: CMakeFiles/MatrixDotNet_Core_Intrinsics.dir/build.make
libMatrixDotNet_Core_Intrinsics.so: CMakeFiles/MatrixDotNet_Core_Intrinsics.dir/link.txt
	@$(CMAKE_COMMAND) -E cmake_echo_color --switch=$(COLOR) --green --bold --progress-dir=/home/nikolay/RiderProjects/MatrixDotNet/src/MatrixDotNet.Core.Intrinsics/cmake-build-debug/CMakeFiles --progress-num=$(CMAKE_PROGRESS_3) "Linking CXX shared library libMatrixDotNet_Core_Intrinsics.so"
	$(CMAKE_COMMAND) -E cmake_link_script CMakeFiles/MatrixDotNet_Core_Intrinsics.dir/link.txt --verbose=$(VERBOSE)

# Rule to build all files generated by this target.
CMakeFiles/MatrixDotNet_Core_Intrinsics.dir/build: libMatrixDotNet_Core_Intrinsics.so

.PHONY : CMakeFiles/MatrixDotNet_Core_Intrinsics.dir/build

CMakeFiles/MatrixDotNet_Core_Intrinsics.dir/clean:
	$(CMAKE_COMMAND) -P CMakeFiles/MatrixDotNet_Core_Intrinsics.dir/cmake_clean.cmake
.PHONY : CMakeFiles/MatrixDotNet_Core_Intrinsics.dir/clean

CMakeFiles/MatrixDotNet_Core_Intrinsics.dir/depend:
	cd /home/nikolay/RiderProjects/MatrixDotNet/src/MatrixDotNet.Core.Intrinsics/cmake-build-debug && $(CMAKE_COMMAND) -E cmake_depends "Unix Makefiles" /home/nikolay/RiderProjects/MatrixDotNet/src/MatrixDotNet.Core.Intrinsics /home/nikolay/RiderProjects/MatrixDotNet/src/MatrixDotNet.Core.Intrinsics /home/nikolay/RiderProjects/MatrixDotNet/src/MatrixDotNet.Core.Intrinsics/cmake-build-debug /home/nikolay/RiderProjects/MatrixDotNet/src/MatrixDotNet.Core.Intrinsics/cmake-build-debug /home/nikolay/RiderProjects/MatrixDotNet/src/MatrixDotNet.Core.Intrinsics/cmake-build-debug/CMakeFiles/MatrixDotNet_Core_Intrinsics.dir/DependInfo.cmake --color=$(COLOR)
.PHONY : CMakeFiles/MatrixDotNet_Core_Intrinsics.dir/depend
