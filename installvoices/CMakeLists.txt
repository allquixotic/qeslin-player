cmake_minimum_required(VERSION 3.0.0 FATAL_ERROR)

################### Variables. ####################
# Change if you want modify path or other values. #
###################################################

set(PROJECT_NAME InstallVoices)
# Output Variables
set(OUTPUT_DEBUG \$(SolutionDir)\$(Platform)/)
set(OUTPUT_REL \$(SolutionDir)\$(Platform)/)
# Folders files
set(CPP_DIR_1 ./)
set(CPP_DIR_2 ./)
set(CPP_DIR_3 ./)
set(HEADER_DIR_1 )

############## CMake Project ################
#        The main options of project        #
#############################################

project(${PROJECT_NAME} CXX)

# Define Release by default.
if(NOT CMAKE_BUILD_TYPE)
  set(CMAKE_BUILD_TYPE "Release")
  message(STATUS "Build type not specified: Use Release by default.")
endif(NOT CMAKE_BUILD_TYPE)

# Definition of Macros
add_definitions(
   -D_CONSOLE 
   -DUNICODE
   -D_UNICODE
)

############## Artefacts Output #################
# Defines outputs , depending Debug or Release. #
#################################################

if(CMAKE_BUILD_TYPE STREQUAL "Debug")
  set(CMAKE_LIBRARY_OUTPUT_DIRECTORY "${CMAKE_BINARY_DIR}/${OUTPUT_DEBUG}")
  set(CMAKE_ARCHIVE_OUTPUT_DIRECTORY "${CMAKE_BINARY_DIR}/${OUTPUT_DEBUG}")
  set(CMAKE_EXECUTABLE_OUTPUT_DIRECTORY "${CMAKE_BINARY_DIR}/${OUTPUT_DEBUG}")
else()
  set(CMAKE_LIBRARY_OUTPUT_DIRECTORY "${CMAKE_BINARY_DIR}/${OUTPUT_REL}")
  set(CMAKE_ARCHIVE_OUTPUT_DIRECTORY "${CMAKE_BINARY_DIR}/${OUTPUT_REL}")
  set(CMAKE_EXECUTABLE_OUTPUT_DIRECTORY "${CMAKE_BINARY_DIR}/${OUTPUT_REL}")
endif()

################# Flags ################
# Defines Flags for Windows and Linux. #
########################################

if(MSVC)
   set(CMAKE_CXX_FLAGS_DEBUG "${CMAKE_CXX_FLAGS_DEBUG} /W3 /MDd /Od /Zi /EHsc")
   set(CMAKE_CXX_FLAGS_RELEASE "${CMAKE_CXX_FLAGS_RELEASE} /W3 /GL /Zi /EHsc")
endif(MSVC)
if(NOT MSVC)
   set(CMAKE_CXX_FLAGS "${CMAKE_CXX_FLAGS} -std=c++11")
   if ("${CMAKE_CXX_COMPILER_ID}" STREQUAL "Clang")
       set (CMAKE_CXX_FLAGS "${CMAKE_CXX_FLAGS} -stdlib=libc++")
   endif()
endif(NOT MSVC)

################ Files ################
#   --   Add files to project.   --   #
#######################################

file(GLOB SRC_FILES
    ${CPP_DIR_1}/*.cpp
    ${HEADER_DIR_1}/*.h
)
# Add executable to build.
add_executable(${PROJECT_NAME}
   ${SRC_FILES}
)

if(MSVC)
   find_package(spdlog CONFIG REQUIRED)
   find_package(aws-cpp-sdk-text-to-speech CONFIG REQUIRED)
   find_package(aws-cpp-sdk-polly CONFIG REQUIRED)
   find_package(aws-cpp-sdk-core CONFIG REQUIRED)
   find_package(tinyxml2 CONFIG REQUIRED)
   find_package(SDL2 CONFIG REQUIRED)
   target_link_libraries(${PROJECT_NAME} spdlog::spdlog tinyxml2::tinyxml2 aws-cpp-sdk-text-to-speech aws-cpp-sdk-core aws-cpp-sdk-polly SDL2::SDL2 SDL2::SDL2main)
endif(MSVC)

