cmake_minimum_required(VERSION 3.16)
project(MatrixDotNet_Core_Intrinsics)

set(CMAKE_CXX_STANDARD 17)

add_library(MatrixDotNet_Core_Intrinsics SHARED src/library.cpp headers/library.h headers/StringBuilder.cpp src/StringBuilder.h)