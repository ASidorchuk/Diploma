cmake_minimum_required(VERSION 3.2)

project(Fractal)

if (MSVC)
   add_compile_options(/W4)
else()
   add_compile_options(-Wall -Wextra -Wpedantic)
endif()

file(GLOB source_list "*.cpp" "*.h")

find_package( OpenCV REQUIRED )
include_directories( ${OpenCV_INCLUDE_DIRS} )

add_executable(Fractal ${source_list})
target_link_libraries( Fractal ${OpenCV_LIBS} )