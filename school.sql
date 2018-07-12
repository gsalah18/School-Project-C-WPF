-- phpMyAdmin SQL Dump
-- version 4.5.5.1
-- http://www.phpmyadmin.net
--
-- Host: 127.0.0.1
-- Generation Time: May 02, 2017 at 07:30 AM
-- Server version: 5.7.11
-- PHP Version: 5.6.19

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `school`
--

-- --------------------------------------------------------

--
-- Table structure for table `cource`
--

CREATE TABLE `cource` (
  `cource_id` int(11) NOT NULL,
  `cource_name` varchar(50) CHARACTER SET latin1 NOT NULL,
  `cource_book` varchar(1000) CHARACTER SET latin1 NOT NULL DEFAULT ''''''
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `cource`
--

INSERT INTO `cource` (`cource_id`, `cource_name`, `cource_book`) VALUES
(11, 'nw', '\'\''),
(15041101, 'C++', 'ptuk.edu.ps'),
(15041102, 'Object Oriented', 'https://drive.google.com/file/d/0B9uo40nO2WiKOTBKN2ZGT1ZqU2s/view?usp=sharing');

-- --------------------------------------------------------

--
-- Table structure for table `enroll`
--

CREATE TABLE `enroll` (
  `en_std_id` int(11) NOT NULL,
  `en_cr_id` int(11) NOT NULL,
  `en_tch_id` int(11) NOT NULL
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

--
-- Dumping data for table `enroll`
--

INSERT INTO `enroll` (`en_std_id`, `en_cr_id`, `en_tch_id`) VALUES
(20141, 15041102, 102),
(20141, 15041101, 101),
(20151, 15041102, 102),
(20151, 15041101, 101),
(20151, 11, 101);

-- --------------------------------------------------------

--
-- Table structure for table `maneger`
--

CREATE TABLE `maneger` (
  `mang_id` int(11) NOT NULL,
  `mang_password` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `maneger`
--

INSERT INTO `maneger` (`mang_id`, `mang_password`) VALUES
(1, 123),
(1, 123);

-- --------------------------------------------------------

--
-- Table structure for table `marks`
--

CREATE TABLE `marks` (
  `mark_std_id` int(11) NOT NULL,
  `mark_cource_id` int(11) NOT NULL,
  `mark_value` int(11) NOT NULL,
  `pass` varchar(6) CHARACTER SET latin1 NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `marks`
--

INSERT INTO `marks` (`mark_std_id`, `mark_cource_id`, `mark_value`, `pass`) VALUES
(20141, 15041102, 93, 'Passed'),
(20151, 15041102, 86, 'Passed'),
(20141, 15041101, 91, 'Passed'),
(20151, 15041101, 85, 'Passed'),
(20141, 15041102, 95, 'Passed');

-- --------------------------------------------------------

--
-- Table structure for table `students`
--

CREATE TABLE `students` (
  `std_id` int(11) NOT NULL,
  `std_name` varchar(50) NOT NULL,
  `std_password` varchar(100) NOT NULL,
  `std_card_id` int(11) NOT NULL,
  `std_major` varchar(50) NOT NULL,
  `std_bdate` year(4) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `students`
--

INSERT INTO `students` (`std_id`, `std_name`, `std_password`, `std_card_id`, `std_major`, `std_bdate`) VALUES
(20141, 'Salah Hadaydeh', '401465885', 401465885, 'Applied Computing', 1996),
(20151, 'Islam', '111', 403456852, 'Applied Computing', 1997);

-- --------------------------------------------------------

--
-- Table structure for table `teacher`
--

CREATE TABLE `teacher` (
  `tch_id` int(11) NOT NULL,
  `tch_name` varchar(50) NOT NULL,
  `tch_password` varchar(150) NOT NULL DEFAULT '12345',
  `tch_degree` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `teacher`
--

INSERT INTO `teacher` (`tch_id`, `tch_name`, `tch_password`, `tch_degree`) VALUES
(101, 'Hadi Khalelia', '12345', 'Master'),
(102, 'Eman Droubi', '12345', 'Doctor');

-- --------------------------------------------------------

--
-- Table structure for table `teachs`
--

CREATE TABLE `teachs` (
  `teacher_id` int(11) NOT NULL,
  `course_id` int(11) NOT NULL
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

--
-- Dumping data for table `teachs`
--

INSERT INTO `teachs` (`teacher_id`, `course_id`) VALUES
(102, 15041102),
(101, 15041101),
(101, 11);

-- --------------------------------------------------------

--
-- Table structure for table `videos`
--

CREATE TABLE `videos` (
  `vid_id` int(11) NOT NULL,
  `vid_name` varchar(100) NOT NULL,
  `vid_link` varchar(500) NOT NULL
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

--
-- Dumping data for table `videos`
--

INSERT INTO `videos` (`vid_id`, `vid_name`, `vid_link`) VALUES
(1, 'Java Tutorial', 'https://www.youtube.com/watch?v=Hl-zzrqQoSE&list=PLFE2CE09D83EE3E28'),
(2, 'C++ Tutorial', 'https://www.youtube.com/watch?v=tvC1WCdV1XU&list=PLAE85DE8440AA6B83'),
(3, 'fff', 'www.ptuk.edu.ps');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `cource`
--
ALTER TABLE `cource`
  ADD PRIMARY KEY (`cource_id`),
  ADD UNIQUE KEY `cource_name` (`cource_name`),
  ADD KEY `cource_id` (`cource_id`);

--
-- Indexes for table `students`
--
ALTER TABLE `students`
  ADD PRIMARY KEY (`std_id`),
  ADD UNIQUE KEY `std_name` (`std_name`),
  ADD UNIQUE KEY `std_card_id` (`std_card_id`);

--
-- Indexes for table `teacher`
--
ALTER TABLE `teacher`
  ADD PRIMARY KEY (`tch_id`),
  ADD UNIQUE KEY `tch_name` (`tch_name`);

--
-- Indexes for table `videos`
--
ALTER TABLE `videos`
  ADD PRIMARY KEY (`vid_id`),
  ADD UNIQUE KEY `vid_name` (`vid_name`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `videos`
--
ALTER TABLE `videos`
  MODIFY `vid_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
